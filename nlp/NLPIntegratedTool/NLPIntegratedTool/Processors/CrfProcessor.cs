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

        public static string Pos2Crf(string posFile)
        {
            if (string.IsNullOrEmpty(posFile))
            {
                throw new Exception("Please specify pos file.");
            }
            posFile = posFile.Trim("\"".ToCharArray());

            if (!File.Exists(posFile))
            {
                throw new Exception("Pos file doesn't exist.");
            }

            var posCrfFile = Path.ChangeExtension(posFile, ".poscrf");

            var arguments = string.Format("-t pos2crf -pf \"{0}\" -pcf \"{1}\"", posFile, posCrfFile);
            var str = ProcessEx.Execute(ExeDir, ExePath, arguments);

            LogHelper.Log(str);

            if (File.Exists(posCrfFile))
            {
                LogHelper.Log("PosCrf output: " + posCrfFile);
                LogHelper.Enter();
                return posCrfFile;
            }
            else
            {
                throw new Exception("Failed to convert pos to crf.");
            }
        }
    }
}
