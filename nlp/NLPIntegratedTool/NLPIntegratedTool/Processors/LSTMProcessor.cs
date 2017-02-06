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
        private static string ExeDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Exe", "NNCRFSegmentor");

        private static string ExePath = Path.Combine(ExeDir, "NNCRFSegmentor.exe");

        public static string DefaultModelFile = Path.Combine(ExeDir, "model");

        public static string Lstm2FinalOutput(string lstmFile, string modelFile = null)
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

            if (string.IsNullOrEmpty(modelFile))
            {
                modelFile = DefaultModelFile;
            }

            var finalOutputFile = Path.ChangeExtension(lstmFile, ".output");

            var arguments = string.Format("-test \"{0}\" -model \"{1}\" -output \"{2}\"", lstmFile, modelFile, finalOutputFile);
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
