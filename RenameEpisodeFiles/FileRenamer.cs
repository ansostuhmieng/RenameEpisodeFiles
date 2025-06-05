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
