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
            Split5000();
            CrfConverter.Convert(@".\Data\CRF\input.txt", @".\Data\CRF\out.txt", @".\Data\CRF\wordBreakerInput.txt");

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
