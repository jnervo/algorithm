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
                var outptuDir = @"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\sentence_count_adjust\output";
                foreach (var type in new string[] { "dev", "test", "train" })
                {
                    var labelInputFile = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\sentence_count_adjust\{0}.labeled", type);
                    var segInputFile = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\sentence_count_adjust\{0}.seg", type);
                    var posInputFile = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\sentence_count_adjust\{0}.pos", type);

                    new CrfConverter().Convert2Crf(labelInputFile, segInputFile, posInputFile, outptuDir, type, true);
                }
            }
            else
            {
                switch (cmdParameters.Type.ToLower())
                {
                    case "crfsplit":
                        if (string.IsNullOrEmpty(cmdParameters.CrfFile) || !File.Exists(cmdParameters.CrfFile))
                        {
                            Console.WriteLine("CrfFile is null or not exist.");
                            return;
                        }
                        new CrfConverter().ConvertCrf2TagFiles(cmdParameters.CrfFile);
                        break;

                    case "pos2crf":
                        if (string.IsNullOrEmpty(cmdParameters.PosFile) || !File.Exists(cmdParameters.PosFile))
                        {
                            Console.WriteLine("PosFile is null or not exist.");
                            return;
                        }
                        new CrfConverter().ConvertPos2Crf(cmdParameters.PosFile, cmdParameters.PosCrfFile);
                        break;
                    case "atomfeat2feattemp":
                        if (string.IsNullOrEmpty(cmdParameters.PosCrfFile) || !File.Exists(cmdParameters.PosCrfFile))
                        {
                            Console.WriteLine("PosCrfFile is null or not exist.");
                            return;
                        }
                        if (string.IsNullOrEmpty(cmdParameters.FeatureTempFile) || !File.Exists(cmdParameters.FeatureTempFile))
                        {
                            Console.WriteLine("FeatureTempFile is null or not exist.");
                            return;
                        }
                        new Atomfeat2featTemp().ConvertFeature(cmdParameters.PosCrfFile, cmdParameters.FeatureTempFile, cmdParameters.FeatureTempOutputFile);
                        break;
                    case "all":

                        if (string.IsNullOrEmpty(cmdParameters.LabelFile) || !File.Exists(cmdParameters.LabelFile))
                        {
                            Console.WriteLine("LabelFile is null or not exist.");
                            return;
                        }
                        if (string.IsNullOrEmpty(cmdParameters.SegFile) || !File.Exists(cmdParameters.SegFile))
                        {
                            Console.WriteLine("SegFile is null or not exist.");
                            return;
                        }
                        if (string.IsNullOrEmpty(cmdParameters.PosFile) || !File.Exists(cmdParameters.PosFile))
                        {
                            Console.WriteLine("PosFile is null or not exist.");
                            return;
                        }
                        new CrfConverter().Convert2Crf(
                            cmdParameters.LabelFile,
                            cmdParameters.SegFile,
                            cmdParameters.PosFile,
                            cmdParameters.Directory,
                            cmdParameters.FileName,
                            cmdParameters.NeedToSplitTags);
                        break;
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
                Name = "PosCrfFile",
                ShortName = "pcf",
                Help = "[OPTIONAL] The absolut path of the output file of PosCrfFile."
            )]
            public string PosCrfFile = null;

            [CommandLineParameter(
                Name = "FeatureTempFile",
                ShortName = "ft",
                Help = "[OPTIONAL] The absolut path of the output file of FeatureTempFile."
            )]
            public string FeatureTempFile = null;

            [CommandLineParameter(
                Name = "FeatureTempOutputFile",
                ShortName = "ftout",
                Help = "[OPTIONAL] The absolut path of the output file of FeatureTempOutputFile."
            )]
            public string FeatureTempOutputFile = null;

            [CommandLineParameter(
                Name = "Directory",
                ShortName = "d",
                Help = "[OPTIONAL] Output directory, default value is current directory."
            )]
            public string Directory = ".";

            [CommandLineParameter(
                Name = "CrfOutputFile",
                ShortName = "crf",
                Help = "[REQUIRED] The absolut path of the CrfOutputFile."
            )]
            public string CrfFile = null;

            [CommandLineParameter(
                Name = "FileName",
                ShortName = "fn",
                Help = "[REQUIRED] The FileName of output files."
            )]
            public string FileName = "output";

            [CommandLineParameter(
                Name = "NeedToSplitTags",
                ShortName = "split",
                Help = "[OPTIONAL] The absolut path of the CrfOutputFile. Defaultly, it will use LabelFile + '.crf' as the output path. "
            )]
            public bool NeedToSplitTags = true;

        }

        private void Move500FromDev2Train()
        {
            var labelInputFile = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\{0}.txt", "dev");
            var segInputFile = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\seg{0}.txt", "dev");
            var posInputFile = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\pos{0}.txt", "dev");

            var labelInputFile2 = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\{0}.txt", "train");
            var segInputFile2 = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\seg{0}.txt", "train");
            var posInputFile2 = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\pos{0}.txt", "train");


            //read
            var crfConverter = new CrfConverter();
            var labelingResults = File.ReadAllLines(labelInputFile).ToList();
            var segResults = crfConverter.ReadSegInputV1(segInputFile);
            var posInfoList = crfConverter.ReadPosInfoList(posInputFile);

            var labelingResults2 = File.ReadAllLines(labelInputFile2).ToList();
            var segResults2 = crfConverter.ReadSegInputV1(segInputFile2);
            var posInfoList2 = crfConverter.ReadPosInfoList(posInputFile2);

            Random random = new Random();
            for (int i = 0; i < 500; i++)
            {
                var index = random.Next(labelingResults.Count);

                labelingResults2.Add(labelingResults[index]);
                segResults2.Add(segResults[index]);
                posInfoList2.Add(posInfoList[index]);

                labelingResults.RemoveAt(index);
                segResults.RemoveAt(index);
                posInfoList.RemoveAt(index);
            }

            var labelOutputFile = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\moved\{0}.txt", "dev");
            var segOutputFile = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\moved\seg{0}.txt", "dev");
            var posOutputFile = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\moved\pos{0}.txt", "dev");

            File.WriteAllLines(labelOutputFile, labelingResults);
            File.WriteAllLines(segOutputFile, segResults.Select(q => string.Join("\t", q)));
            File.WriteAllLines(posOutputFile, posInfoList.Select(q => string.Join("\t", q.Words.Select(w => string.Join("/", w.Word, w.Pos)))));

            var labelOutputFile2 = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\moved\{0}.txt", "train");
            var segOutputFile2 = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\moved\seg{0}.txt", "train");
            var posOutputFile2 = string.Format(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\moved\pos{0}.txt", "train");

            File.WriteAllLines(labelOutputFile2, labelingResults2);
            File.WriteAllLines(segOutputFile2, segResults2.Select(q => string.Join("\t", q)));
            File.WriteAllLines(posOutputFile2, posInfoList2.Select(q => string.Join("\t", q.Words.Select(w => string.Join("/", w.Word, w.Pos)))));


        }

        /// <summary>
        /// 5000 lines, split into 3 files:
        /// 1,6,11,16...    File1
        /// 2,7,12,17...    File2
        /// Others          File3
        /// </summary>
        void Split5000()
        {
            var lines = File.ReadAllLines(@"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\hotel1a.txt", Encoding.GetEncoding("gb2312"));

            var file1 = new List<string>();
            var file2 = new List<string>();
            var file3 = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                if (i % 10 == 0)
                {
                    file1.Add(lines[i]);
                }
                else if (i % 10 == 1 || i % 10 == 2)
                {
                    file2.Add(lines[i]);
                }
                else
                {
                    file3.Add(lines[i]);
                }
            }
            //7:2:1
            File.WriteAllLines("hotel_500.txt", file1);
            File.WriteAllLines("hotel_1000.txt", file2);
            File.WriteAllLines("hotel_3500.txt", file3);
        }

        private static void MoveSentence()
        {
            var file1 = @"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\sentence_count_adjust\dev.labeled";
            var file2 = @"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\sentence_count_adjust\test.labeled";
            var file3 = @"D:\yuzhu\src\git\github\algorithm\nlp\NlpFileConverter\NlpFileConverter\Data\CRF\sentence_count_adjust\train.labeled";

            var lines1 = File.ReadAllLines(file1);
            var lines2 = File.ReadAllLines(file2);
            var lines3 = File.ReadAllLines(file3);

            var newLines1 = new List<string>();
            var newLines2 = new List<string>();
            var newLines3 = new List<string>(lines3);

            for (int i = 0; i < lines1.Length; i++)
            {
                if (i % 2 == 0)
                {
                    newLines1.Add(lines1[i]);
                }
                else
                {
                    newLines3.Add(lines1[i]);
                }
            }

            for (int i = 0; i < lines2.Length; i++)
            {
                if (i % 2 == 0)
                {
                    newLines2.Add(lines2[i]);
                }
                else
                {
                    newLines3.Add(lines2[i]);
                }
            }
            File.WriteAllLines(file1, newLines1);
            File.WriteAllLines(file2, newLines2);
            File.WriteAllLines(file3, newLines3);
        }
    }
}
