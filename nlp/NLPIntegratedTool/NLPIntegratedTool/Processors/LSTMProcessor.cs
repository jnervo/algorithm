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
                        return Path.Combine(ExeDir, "model_mobile");
                    case ModelType.Hotel:
                        return Path.Combine(ExeDir, "model_hotel");
                    default:
                        throw new Exception("Unexpected ModelType:" + CurrentModelType.ToString());
                }
            }
        }
        internal static void SetModelType(ModelType modelType)
        {
            CurrentModelType = modelType;
        }

        private static string ExeDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Exe", "NNCRFSegmentor");

        private static string ExePath = Path.Combine(ExeDir, "NNCRFSegmentor.exe");

        public static ModelType CurrentModelType = ModelType.Mobile;

        public static bool Lstm2FinalOutput(ProcessResult result)
        {
            var lstmFile = result.LstmFile;
            var outputFile = result.OutputFile;

            if (string.IsNullOrEmpty(lstmFile))
            {
                throw new Exception("Please specify LSTM file.");
            }
            lstmFile = lstmFile.Trim("\"".ToCharArray());

            if (!File.Exists(lstmFile))
            {
                throw new Exception("LSTM file doesn't exist.");
            }

            var arguments = string.Format("-test \"{0}\" -model \"{1}\" -output \"{2}\"", lstmFile, ModelFilePath, outputFile);
            var str = ProcessEx.Execute(ExeDir, ExePath, arguments);

            LogHelper.Log(str);

            if (File.Exists(outputFile))
            {
                LogHelper.Log("Final output: " + outputFile);
                LogHelper.Enter();
                return true;
            }
            else
            {
                throw new Exception("Failed to convert lstm to final output.");
            }
        }
    }
}
