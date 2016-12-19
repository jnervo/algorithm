using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NlpFileConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var type in new string[] { "dev", "test", "train" })
            {
                var inputFile = string.Format(@".\Data\CRF\{0}\{0}.txt", type);
                var segInputFile = string.Format(@".\Data\CRF\{0}\seg{0}.txt", type);
                var posInputFile = string.Format(@".\Data\CRF\{0}\pos{0}.txt", type);

                var outputFile = string.Format(@".\Data\CRF\{0}\{0}.output", type);

                CrfConverter.Convert2Crf(inputFile, segInputFile, posInputFile, outputFile);
            }

            //Split5000();
            //StanfordConverter.ConvertParserResult2Crf(@".\Data\Stanford\parser.txt", @".\Data\Stanford\out.convert", @".\Data\Stanford\seg.txt");
        }

        /// <summary>
        /// 5000 lines, split into 3 files:
        /// 1,6,11,16...    File1
        /// 2,7,12,17...    File2
        /// Others          File3
        /// </summary>
        static void Split5000()
        {
            var lines = File.ReadAllLines(@"D:\yuzhu\src\git\github\tools\Tools\Parsers\手机原始.txt");

            var file1 = new List<string>();
            var file2 = new List<string>();
            var file3 = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                if (i % 5 == 0)
                {
                    file1.Add(lines[i]);
                }
                else if (i % 5 == 1)
                {
                    file2.Add(lines[i]);
                }
                else
                {
                    file3.Add(lines[i]);
                }
            }
            File.WriteAllLines("1000_1.txt", file1);
            File.WriteAllLines("1000_2.txt", file2);
            File.WriteAllLines("3000_1.txt", file3);
        }
    }
}
