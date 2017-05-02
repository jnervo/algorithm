using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NlpFileConverter
{
    public class TagExtractor
    {
        private static Regex _aRegex = new Regex("\\<a\\>(?<Value>.*?)\\</a\\>");
        private static Regex _eRegex = new Regex("\\<e\\>(?<Value>.*?)\\</e\\>");
        private static Regex _expRegex = new Regex("\\<exp.*?\\>(?<Value>.*?)\\</exp.*?\\>");


        private static string[] _tags = new string[] { "a", "e", "exp-1", "exp-2", "exp-3", "exp-4" };

        public void Extract(string inputFile, string outputFile = null)
        {
            if (string.IsNullOrEmpty(outputFile))
            {
                outputFile = Path.ChangeExtension(inputFile, ".out");
            }

            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                foreach (var line in File.ReadAllLines(inputFile, Encoding.GetEncoding("gb2312")))
                {
                    var aList = MatchAll(line, _aRegex);
                    var eList = MatchAll(line, _eRegex);
                    var expList = MatchAll(line, _expRegex);

                    sw.WriteLine(string.Join("\t", aList));
                    sw.WriteLine(string.Join("\t", eList));
                    sw.WriteLine(string.Join("\t", expList));
                }
            }
        }

        private List<string> MatchAll(string line, Regex regex)
        {
            List<string> ret = new List<string>();
            var collection = regex.Matches(line);
            foreach (Match match in collection)
            {
                ret.Add(match.Groups["Value"].Value);
            }
            return ret;
        }

        public void MovePunctuationIntoTag(string inputFile, string outputFile = null)
        {
            if (string.IsNullOrEmpty(outputFile))
            {
                outputFile = Path.ChangeExtension(inputFile, ".out");
            }

            var punctuations = new string[] { "，", "。" };
            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                foreach (var line in File.ReadAllLines(inputFile, Encoding.GetEncoding("gb2312")))
                {
                    var newLine = line;
                    foreach (var tag in _tags)
                    {
                        var closeTag = string.Format("</{0}>", tag);

                        foreach (var punctuation in punctuations)
                        {
                            var oldStr = closeTag + punctuation;
                            var newStr = punctuation + closeTag;

                            newLine = newLine.Replace(oldStr, newStr);
                        }
                    }
                    sw.WriteLine(newLine);
                }
            }
        }

    }
}
