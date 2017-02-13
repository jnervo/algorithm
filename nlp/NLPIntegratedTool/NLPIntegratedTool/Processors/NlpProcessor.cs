using NLPIntegratedTool.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPIntegratedTool
{
    public class NlpProcessor
    {
        private static string ExeDir = AppDomain.CurrentDomain.BaseDirectory;

        public static bool Seg(ProcessResult result)
        {
            var inputFile = result.InputFile;
            var outputFile = result.SegFile;

            if (string.IsNullOrEmpty(inputFile))
            {
                throw new Exception("Please specify input file.");
            }
            inputFile = inputFile.Trim("\"".ToCharArray());

            if (!File.Exists(inputFile))
            {
                throw new Exception("Input file doesn't exist.");
            }

            var cmdTemplatePath = "seg_template.cmd";
            var cmdPath = "seg_run.cmd";

            File.WriteAllText(cmdPath, File.ReadAllText(cmdTemplatePath).Replace("##INPUTFILE##", inputFile).Replace("##OUTPUTFILE##", outputFile));

            var str = ProcessEx.Execute(AppDomain.CurrentDomain.BaseDirectory, cmdPath);

            LogHelper.Log(str);

            if (File.Exists(outputFile))
            {
                LogHelper.Log("Seg output: " + outputFile);
                LogHelper.Enter();
                return true;
            }
            else
            {
                throw new Exception("Failed to seg input file.");
            }
        }

        public static bool Pos(ProcessResult result)
        {
            var segFile = result.SegFile;
            var outputFile = result.PosFile;

            if (string.IsNullOrEmpty(segFile))
            {
                throw new Exception("Please specify seg file.");
            }
            segFile = segFile.Trim("\"".ToCharArray());

            if (!File.Exists(segFile))
            {
                throw new Exception("Seg file doesn't exist.");
            }

            var cmdTemplatePath = "pos_template.cmd";
            var cmdPath = "pos_run.cmd";

            File.WriteAllText(cmdPath, File.ReadAllText(cmdTemplatePath).Replace("##INPUTFILE##", segFile).Replace("##OUTPUTFILE##", outputFile));

            var str = ProcessEx.Execute(AppDomain.CurrentDomain.BaseDirectory, cmdPath);

            LogHelper.Log(str);

            if (File.Exists(outputFile))
            {
                LogHelper.Log("Pos output: " + outputFile);
                LogHelper.Enter();
                return true;
            }
            else
            {
                throw new Exception("Failed to pos seg file.");
            }
        }
    }
}
