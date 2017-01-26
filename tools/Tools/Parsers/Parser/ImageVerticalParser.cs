using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parsers.Parser
{
    public class ImageVerticalParser
    {
        public static void GetThumbnailIdsForAllTopics()
        {
            var lines = File.ReadAllLines(@"D:\yuzhu\src\git\github\algorithm\tools\Tools\Parsers\Data\InaugurationQuries.txt");


            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var line in lines)
            {
                var tokens = line.Split('\t');

                var topic = tokens[0];
                var imageDetailPage = tokens[1];
                if (!imageDetailPage.StartsWith("http"))
                {
                    continue;
                }
                var content = Scraper.QuickDownloadUrlContent(imageDetailPage + "&format=pbxml");

                var regex = new Regex("MMWebAnswer/MMImageWebAnswer[\\s\\S.]*?Thumbnails[\\s\\S.]*?\\\"SourceURL\\\" \\: \\\"(?<ThumbnailId>.*?)\\\"");

                MatchCollection matches = regex.Matches(content);
                if (matches.Count == 2)
                {
                    Match match = matches[1];
                    var thumbnailId = match.Groups["ThumbnailId"].Value;
                    dic[topic] = thumbnailId;
                }
                else
                {
                    dic[topic] = imageDetailPage;
                }
                Console.WriteLine(dic.Count);
            }
            File.WriteAllLines("GetThumbnailIdsForAllTopics_InaugurationQuries.txt", dic.Select(d => string.Join("\t", d.Key, d.Value)));

        }
    }
}
