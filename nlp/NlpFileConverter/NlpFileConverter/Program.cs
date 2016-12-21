using MS.Msn.Runtime;
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
            CommandLine<CmdParameters> cmdLine = new CommandLine<CmdParameters>();
            CmdParameters cmdParameters = cmdLine.Parse(args);

            if (cmdLine.NeedsHelp)
            {
                cmdLine.Help();
                return;
            }

            if (args.Length == 0) // no args, run default function
            {
                var outptuDir = @"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\output";
                foreach (var type in new string[] { "dev", "test", "train" })
                {
                    var labelInputFile = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\{0}.txt", type);
                    var segInputFile = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\seg{0}.txt", type);
                    var posInputFile = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\pos{0}.txt", type);

                    CrfConverter.Convert2Crf(labelInputFile, segInputFile, posInputFile, outptuDir, type, true);
                }
            }
        }

        class CmdParameters
        {
            [CommandLineParameter(
                Name = "type",
                ShortName = "t",
                Help = "[REQUIRED] The Type of convert."
            )]
            public string Type = null;

            [CommandLineParameter(
                Name = "LabelFile",
                ShortName = "lf",
                Help = "[REQUIRED] The absolut path of the LabelFile. e.g. dev.txt, test.txt, train.txt"
            )]
            public string LabelFile = null;

            [CommandLineParameter(
                Name = "SegFile",
                ShortName = "sf",
                Help = "[REQUIRED] The absolut path of the SegFile. e.g. segdev.txt, segtest.txt, segtrain.txt"
            )]
            public string SegFile = null;

            [CommandLineParameter(
                Name = "PosFile",
                ShortName = "pf",
                Help = "[REQUIRED] The absolut path of the PosFile. e.g. posdev.txt, postest.txt, postrain.txt"
            )]
            public string PosFile = null;

            [CommandLineParameter(
                Name = "Directory",
                ShortName = "d",
                Help = "[OPTIONAL] Output directory, default value is current directory."
            )]
            public string Directory = ".";

            [CommandLineParameter(
                Name = "CrfOutputFile",
                ShortName = "crf",
                Help = "[OPTIONAL] The absolut path of the CrfOutputFile. Defaultly, it will use LabelFile + '.crf' as the output path. "
            )]
            public string CrfOutputFile = null;

            [CommandLineParameter(
                Name = "NeedToSplitTags",
                ShortName = "split",
                Help = "[OPTIONAL] The absolut path of the CrfOutputFile. Defaultly, it will use LabelFile + '.crf' as the output path. "
            )]
            public bool NeedToSplitTags = true;

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
