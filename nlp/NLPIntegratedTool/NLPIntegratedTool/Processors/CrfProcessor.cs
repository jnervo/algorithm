using NLPIntegratedTool.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPIntegratedTool
{
    public class CrfProcessor
    {
        private static string ExeDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Exe", "CrfConverter");

        private static string ExePath = Path.Combine(ExeDir, "NlpFileConverter.exe");

        private static string Feat_temp2File = Path.Combine(ExeDir, "feat_temp2");

        private static string AdNeuralCharTagFeatFile = Path.Combine(ExeDir, "adNeuralCharTagFeat.py");

        public static bool Pos2Crf(ProcessResult result)
        {
            var posFile = result.PosFile;
            var outputFile = result.PosCrfFile;

            if (string.IsNullOrEmpty(posFile))
            {
                throw new Exception("Please specify pos file.");
            }
            posFile = posFile.Trim("\"".ToCharArray());

            if (!File.Exists(posFile))
            {
                throw new Exception("Pos file doesn't exist.");
            }

            var arguments = string.Format("-t pos2crf -pf \"{0}\" -pcf \"{1}\"", posFile, outputFile);
            var str = ProcessEx.Execute(ExeDir, ExePath, arguments);

            LogHelper.Log(str);

            if (File.Exists(outputFile))
            {
                LogHelper.Log("PosCrf output: " + outputFile);
                LogHelper.Enter();
                return true;
            }
            else
            {
                throw new Exception("Failed to convert pos to crf.");
            }
        }

        public static bool Crf2Lstm(ProcessResult result)
        {
            var posCrfFile = result.PosCrfFile;
            var outputFile = result.LstmFile;

            if (string.IsNullOrEmpty(posCrfFile))
            {
                throw new Exception("Please specify crf file.");
            }
            posCrfFile = posCrfFile.Trim("\"".ToCharArray());

            if (!File.Exists(posCrfFile))
            {
                throw new Exception("Crf file doesn't exist.");
            }

            //Step 1
            var ftout = Path.ChangeExtension(posCrfFile, ".crf2lstm");

            var arguments = string.Format("-t atomfeat2featTemp -pcf \"{0}\" -ft \"{1}\" -ftout \"{2}\"", posCrfFile, Feat_temp2File, ftout);
            var str = ProcessEx.Execute(ExeDir, ExePath, arguments);
            LogHelper.Log(str);

            if (!File.Exists(ftout))
            {
                throw new Exception("Failed to run atomfeat2featTemp");
            }

            //Step 2
            var pythonCmdTemplatePath = Path.Combine(ExeDir, "adNeuralCharTagFeatTemplate.cmd");
            var pythonCmdPath = Path.Combine(ExeDir, "adNeuralCharTagFeat.cmd");
            File.WriteAllText(pythonCmdPath, File.ReadAllText(pythonCmdTemplatePath).Replace("##INPUTFILE##", ftout).Replace("##OUTPUTFILE##", outputFile));

            str = ProcessEx.Execute(ExeDir, pythonCmdPath);
            LogHelper.Log(str);

            if (File.Exists(outputFile))
            {
                LogHelper.Log("Output for LSTM: " + outputFile);
                LogHelper.Enter();
                return true;
            }
            else
            {
                throw new Exception("Failed to run adNeuralCharTagFeat.py");
            }
        }

    }
}
