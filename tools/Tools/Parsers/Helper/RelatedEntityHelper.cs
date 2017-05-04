using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsers.Helper
{
    class RelatedEntityHelper
    {
        public static void Sample()
        {
            var r = new Random();

            var lines = File.ReadAllLines(@"C:\Users\yuzhu\Downloads\RelatedTopics_TOP100K (2).txt")
                .Take(5000).OrderByDescending(l => r.Next(10000)).Take(200);

            var newLines = new List<string>();
            foreach (var line in lines)
            {
                var tokens = line.Split('\t');
                var entities = tokens[1].Split(';').Take(5);

                newLines.Add(string.Join("\t", tokens[0], string.Join(";", entities), tokens[2]));
            }
            File.WriteAllLines("SampledEntity.txt", newLines);
        }

        public static void Statistic()
        {
            var lines = File.ReadAllLines(@"C:\Users\yuzhu\Downloads\RelatedTopics_TOP100K (2).txt");

            var newLines = new List<string>();
            foreach (var line in lines)
            {
                var tokens = line.Split('\t');
                var entities = tokens[1].Split(';').Count();

                newLines.Add(string.Join("\t", line, entities));
            }
            File.WriteAllLines("EntityCount.txt", newLines);
        }
    }
}
