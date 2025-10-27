using Microsoft.Extensions.Logging;
using OpenAI;
using OpenAI.Responses;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace RenameEpisodeFiles
{
    public record FileRenameRecord(string OriginalFileName, string NewFileName, string OriginalFullPath, string NewFullPath, DateTime RenameTime);

    internal class FileRenamer
    {
        private static readonly string HistoryFolder = Path.Combine(
            AppContext.BaseDirectory,
            "History");

        private static void SaveRenameHistory(List<FileRenameRecord> renameHistory)
        {
            Directory.CreateDirectory(HistoryFolder);
            var timestamp = DateTime.Now.ToString("yyyyMMddTHHmmss");
            var historyFile = Path.Combine(HistoryFolder, $"rename_history_{timestamp}.json");

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(renameHistory, jsonOptions);
            File.WriteAllText(historyFile, jsonString);

            Program.Logger.LogInformation($"Rename history saved to: {historyFile}");
        }

        public static int RenameEpisodes(
            string folderPath,
            string showName,
            int seasonNumber,
            int startingEpisode,
            string episodeNamesFile)
        {
            var renameHistory = new List<FileRenameRecord>();

            // Read episode names from file
            var episodeNames = File.ReadAllLines(episodeNamesFile)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToList();

            // Get files ordered by creation time
            var files = new DirectoryInfo(folderPath)
                .GetFiles()
                .OrderBy(f => f.CreationTime)
                .ToList();

            int episodeIndex = startingEpisode - 1;
            int lastEpisode = episodeIndex;

            for (int i = 0; i < files.Count && episodeIndex < episodeNames.Count; i++, episodeIndex++)
            {
                var file = files[i];
                string episodeNumber = (episodeIndex + 1).ToString("D2");
                string newFileName = $"{showName} S{seasonNumber:D2}E{episodeNumber} - {episodeNames[episodeIndex]}{file.Extension}";
                string newFilePath = Path.Combine(folderPath, newFileName);

                File.Move(file.FullName, newFilePath);
                renameHistory.Add(new FileRenameRecord(file.Name, newFileName, file.FullName, newFilePath, DateTime.Now));
                lastEpisode = episodeIndex + 1;
            }

            if (renameHistory.Any())
            {
                SaveRenameHistory(renameHistory);
            }

            return lastEpisode;
        }

        public static async Task RenameEpisodesWithAI(string folderPath, string showName, bool byEpisodeNumber = true)
        {
            // Get all files in the directory
            var allFiles = new DirectoryInfo(folderPath)
                .GetFiles()
                .OrderBy(f => f.CreationTime)
                .ToList();

            var newFileNames = new List<string>();
            const int CHUNK_SIZE = 3;

            var openAIService = Program.OpenAIService;
            if (openAIService == null)
            {
                Program.Logger.LogError("OpenAI Responses service is not configured");
                return;
            }

            // Chunk the AI calls into groups of CHUNK_SIZE files at a time
            for (int i = 0; i < allFiles.Count; i += CHUNK_SIZE)
            {
                var files = allFiles.Skip(i).Take(CHUNK_SIZE).ToArray();
                // Convert to a list of file names as newline-separated strings
                var fileNames = files.Select(f => f.Name).ToList();

                // Convert the list to a single string with each filename on a new line
                string fileNamesString = string.Join(Environment.NewLine, fileNames);

                string prompt = byEpisodeNumber ?
                    $"""
                    You are a TV show episode naming expert. You'll receive a list of filenames to rename.
                    Your task is to return them in the format "{showName}.S<Season Number>E<Episode Number>.<Title>.<Extension>".
                    Get the <Title> from theTVDB, using the {showName} tab for that <Season Number>.
                    Return only the renamed filenames, one per line, without any additional text.
                    Do not include bullets or number prefixes.
                    \n{fileNamesString}
                    """ :
                    $"""
                    You are a TV show episode title expert. You'll receive filenames in the format "<Show Name><Separator><Title>.<Extension>".
                    Your task is to return them in the format "{showName}.S<Season Number>E<Episode Number>.<Title>.<Extension>".
                    Search every Season of {showName} on theTVDB.com using Aired Order to find similar titles.
                    Use the closest matching title and its episode number. Leave season/episode blank if no match found.
                    Return only the renamed filenames, one per line, without any additional text.
                    Do not include bullets or number prefixes.
                    \n{fileNamesString}
                    """;

                try
                {
                    // Use the Responses API with the web-search tool enabled for better matches
                    var options = new ResponseCreationOptions()
                    {
                        Tools = { ResponseTool.CreateWebSearchTool() }
                    };

                    OpenAIResponse response = await openAIService.CreateResponseAsync(prompt, options);

                    if (response == null)
                    {
                        Program.Logger.LogError("OpenAI returned null response");
                        continue;
                    }

                    var combined = new StringBuilder();

                    foreach (ResponseItem item in response.OutputItems)
                    {
                        if (item is MessageResponseItem message)
                        {
                            var text = message.Content?.FirstOrDefault()?.Text;
                            if (!string.IsNullOrEmpty(text))
                            {
                                combined.AppendLine(text);
                                Program.Logger.LogDebug($"[{message.Role}] {text}");
                            }
                        }
                    }

                    var content = combined.ToString().Trim();
                    if (!string.IsNullOrEmpty(content))
                    {
                        Program.Logger.LogDebug($"AI Response: {content}");
                        var fileNameList = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(line => line.Trim())
                            .ToList();

                        newFileNames.AddRange(fileNameList);
                    }
                    else
                    {
                        Program.Logger.LogError("Failed to get content from OpenAI response");
                    }
                }
                catch (Exception ex)
                {
                    Program.Logger.LogError(ex, "Error while getting AI response");
                }
            }

            // Rename files based on AI response
            if (newFileNames.Any())
            {
                RenameFiles(folderPath, allFiles, newFileNames);
            }
        }

        static void RenameFiles(string folderPath, List<FileInfo> files, List<string> newFileNames)
        {
            var renameHistory = new List<FileRenameRecord>();

            for (int i = 0; i < files.Count && i < newFileNames.Count; i++)
            {
                var file = files[i];
                string newFileName = SanitizeFileName(newFileNames[i]);
                string newFilePath = Path.Combine(folderPath, newFileName);

                // Check if the new file name is different before renaming
                if (FileDoesNotExist(file.FullName, newFilePath))
                {
                    Program.Logger.LogInformation($"Renaming '{file.FullName}' to '{newFileName}'");
                    try
                    {
                        File.Move(file.FullName, newFilePath);
                        renameHistory.Add(new FileRenameRecord(file.Name, newFileName, file.FullName, newFilePath, DateTime.Now));
                    }
                    catch (Exception ex)
                    {
                        Program.Logger.LogError(new EventId(), ex, ex.Message);
                        continue;
                    }
                    Program.Logger.LogInformation($"Renamed '{file.FullName}' to '{newFileName}'");
                }
                else
                {
                    Program.Logger.LogInformation($"Skipped '{newFilePath}' as it already exists");
                }
            }

            if (renameHistory.Any())
            {
                SaveRenameHistory(renameHistory);
            }
        }

        public static bool UndoLastRename()
        {
            try
            {
                // Ensure history folder exists
                if (!Directory.Exists(HistoryFolder))
                {
                    Program.Logger.LogInformation("No rename history found to undo");
                    return false;
                }

                // Get the latest history file
                var historyFiles = Directory.GetFiles(HistoryFolder, "rename_history_*.json")
                    .OrderByDescending(f => f)
                    .ToList();

                if (!historyFiles.Any())
                {
                    Program.Logger.LogInformation("No rename history files found to undo");
                    return false;
                }

                var latestHistoryFile = historyFiles.First();
                var jsonString = File.ReadAllText(latestHistoryFile);
                var renameHistory = JsonSerializer.Deserialize<List<FileRenameRecord>>(jsonString);

                if (renameHistory == null || !renameHistory.Any())
                {
                    Program.Logger.LogInformation("Empty rename history in file");
                    return false;
                }

                // Prepare reverse rename operations using full paths
                var filesToRename = new List<FileInfo>();
                var originalNames = new List<string>();
                var originalPaths = new List<string>();

                foreach (var record in renameHistory)
                {
                    if (File.Exists(record.NewFullPath))
                    {
                        filesToRename.Add(new FileInfo(record.NewFullPath));
                        originalNames.Add(Path.GetFileName(record.OriginalFullPath));
                        originalPaths.Add(record.OriginalFullPath);
                    }
                }

                if (!filesToRename.Any())
                {
                    Program.Logger.LogInformation("No files found to undo rename");
                    return false;
                }

                // Actually move files back to their original full paths
                for (int i = 0; i < filesToRename.Count; i++)
                {
                    var file = filesToRename[i];
                    var originalPath = originalPaths[i];
                    try
                    {
                        File.Move(file.FullName, originalPath);
                    }
                    catch (Exception ex)
                    {
                        Program.Logger.LogError(ex, $"Error undoing rename for {file.FullName}");
                    }
                }

                // Delete the history file after successful undo
                File.Delete(latestHistoryFile);
                Program.Logger.LogInformation($"Successfully undid renames from {latestHistoryFile}");
                return true;
            }
            catch (Exception ex)
            {
                Program.Logger.LogError(ex, "Error while trying to undo rename");
                return false;
            }
        }

        public static string ExtractAndWriteEpisodeNames(string inputFilePath)
        {
            var episodeNames = new List<string>();
            var regex = new Regex(@"^S\d{2}E\d{2}\s*(.+)$");

            foreach (var line in File.ReadLines(inputFilePath))
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    // If tab-separated, get the part after the tab
                    var parts = match.Groups[0].Value.Split('\t');
                    if (parts.Length > 1)
                        episodeNames.Add(parts[1].Trim());
                    else
                        episodeNames.Add(match.Groups[1].Value.Trim());
                }
            }

            // Generate output path by inserting "-clean" before the extension
            string dir = Path.GetDirectoryName(inputFilePath);
            string filename = Path.GetFileNameWithoutExtension(inputFilePath);
            string ext = Path.GetExtension(inputFilePath);
            string outputFilePath = Path.Combine(dir, $"{filename}-clean{ext}");

            File.WriteAllLines(outputFilePath, episodeNames);

            return outputFilePath;
        }

        public static string SanitizeFileName(string fileName, char replacement = '_')
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return string.Empty;

            var invalidChars = Path.GetInvalidFileNameChars();
            var sanitized = new StringBuilder(fileName.Length);

            foreach (char c in fileName)
            {
                sanitized.Append(invalidChars.Contains(c) ? replacement : c);
            }

            // Trim trailing spaces or periods (not allowed in Windows)
            return sanitized.ToString().TrimEnd(' ', '.');
        }

        private static bool FileDoesNotExist(string currentFile, string newFilePath)
        {
            return (!string.Equals(currentFile, newFilePath, StringComparison.OrdinalIgnoreCase)) &&
                   !File.Exists(newFilePath);
        }
    }
}
