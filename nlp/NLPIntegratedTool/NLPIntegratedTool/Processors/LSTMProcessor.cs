using NLPIntegratedTool.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPIntegratedTool
{
    public class LstmProcessor
    {
        public enum ModelType
        {
            Mobile,
            Hotel
        }

        public static string ModelFilePath
        {
            get
            {
                switch (CurrentModelType)
                {
                    case ModelType.Mobile:
                        return Path.Combine(ExeDir, "model");
                    case ModelType.Hotel:
                        return Path.Combine(ExeDir, "model");
                    default:
                        return Path.Combine(ExeDir, "model");
                }
            }
        }

        internal static void SetModelType(ModelType modelType)
        {
            CurrentModelType = modelType;
        }

        private static string ExeDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Exe", "NNCRFSegmentor");

        private static string ExePath = Path.Combine(ExeDir, "NNCRFSegmentor.exe");

        private static ModelType CurrentModelType = ModelType.Mobile;

        public static string Lstm2FinalOutput(string lstmFile)
        {
            if (string.IsNullOrEmpty(lstmFile))
            {
                throw new Exception("Please specify LSTM file.");
            }
            lstmFile = lstmFile.Trim("\"".ToCharArray());

            if (!File.Exists(lstmFile))
            {
                throw new Exception("LSTM file doesn't exist.");
            }

            var finalOutputFile = Path.ChangeExtension(lstmFile, ".output");

            var arguments = string.Format("-test \"{0}\" -model \"{1}\" -output \"{2}\"", lstmFile, ModelFilePath, finalOutputFile);
            var str = ProcessEx.Execute(ExeDir, ExePath, arguments);

            LogHelper.Log(str);

            if (File.Exists(finalOutputFile))
            {
                LogHelper.Log("Final output: " + finalOutputFile);
                LogHelper.Enter();
                return finalOutputFile;
            }
            else
            {
                throw new Exception("Failed to convert lstm to final output.");
            }
        }
    }
}
