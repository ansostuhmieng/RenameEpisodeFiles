using Microsoft.Extensions.Logging;
using OpenAI.Net;
using OpenAI.Net.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RenameEpisodeFiles
{
    internal class FileRenamer
    {
       public static int RenameEpisodes(
       string folderPath,
       string showName,
       int seasonNumber,
       int startingEpisode,
       string episodeNamesFile)
        {
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
                lastEpisode = episodeIndex + 1;
            }

            return lastEpisode;
        }

        public static async void RenameEpisodesWithAI(string folderPath, string showName)
        {
            // Get all files in the directory
            var files = new DirectoryInfo(folderPath)
                .GetFiles()
                .OrderBy(f => f.CreationTime)
                .ToList();

            // Convert to a list of file names as newline -separated strings
            var fileNames = files.Select(f => f.Name).ToList();

            // Convert the list to a single string with each filename on a new line
            string fileNamesString = string.Join(Environment.NewLine, fileNames);

            // Make an API call to OpenAI to get the episode names using the OpenAI.Net.Client library
            var openAIService = Program.OpenAIService;
            var response = await openAIService.Chat.Get($"The following list of filenames are in the format of <Show Title><Separator><Season><Episodes><Separator><Optional Title>.<Extension>. I want them to be in the format \"{showName}.S<Season Number>E<Episode Number>.<PascalCase Title>.<Extension>. If the Title isn't in the filename, grab it from theTVDB, using the {showName} DVD Order tab.\r\n{fileNamesString}\r\nGive me back the list of updated filenames", (options) =>
            {
                options.Model = "gpt-3.5-turbo";
                options.MaxTokens = 1000;
                options.Temperature = 0.7;
            });

            if (response.IsSuccess)
            {
                var aiResponseText = response.Result!.Choices[0].Message.Content;
                Program.Logger.LogDebug($"AI Response: {aiResponseText}");
                // Split the response into an array of lines
                var newFileNames = aiResponseText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(line => line.Trim())
                    .ToList();
                // Rename files based on AI response
                for (int i = 0; i < files.Count && i < newFileNames.Count; i++)
                {
                    var file = files[i];
                    string newFileName = newFileNames[i];
                    string newFilePath = Path.Combine(folderPath, newFileName);
                    // Check if the new file name is different before renaming
                    if (!string.Equals(file.FullName, newFilePath, StringComparison.OrdinalIgnoreCase))
                    {
                        File.Move(file.FullName, newFilePath);
                        Program.Logger.LogInformation($"Renamed '{file.Name}' to '{newFileName}'");
                    }
                }
            }
            else
            {
                Program.Logger.LogError(response.ErrorResponse?.Error?.Message);
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

    }
}
