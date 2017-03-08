using NLPIntegratedTool.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NLPIntegratedTool
{
    public class NNPoolingProcessor
    {
        private static string ExeDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Exe", "NNPooling");

        public static string ModelFilePath
        {
            get
            {
                switch (Processor.CurrentModelType)
                {
                    case ModelType.Mobile:
                        return Path.Combine(ExeDir, "phone.model");
                    case ModelType.Hotel:
                        return Path.Combine(ExeDir, "hotel.model");
                    default:
                        return Path.Combine(ExeDir, "phone.model");
                }
            }
        }

        public static string ExeFilePath
        {
            get
            {
                switch (Processor.CurrentModelType)
                {
                    case ModelType.Mobile:
                        return "NNCNNTriTanhPoolLabeler.exe";
                    case ModelType.Hotel:
                        return "NNCNNTriReluPoolLabeler.exe";
                    default:
                        return null;
                }
            }
        }

        public static bool Label(ProcessResult result)
        {
            var segFile = result.SegFile;
            var outputFile = result.LabelFile;

            if (string.IsNullOrEmpty(segFile))
            {
                throw new Exception("Please specify seg file.");
            }
            segFile = segFile.Trim("\"".ToCharArray());

            if (!File.Exists(segFile))
            {
                throw new Exception("Seg file doesn't exist.");
            }
            File.WriteAllLines(segFile, File.ReadAllLines(segFile).Select(l => string.Format("0\t{0}", l.Replace("\t", " "))));

            var cmdTemplatePath = Path.Combine(ExeDir, "pooling_template.cmd");
            var cmdPath = Path.Combine(ExeDir, "pooling_run.cmd");
            var exeFullPath = Path.Combine(ExeDir, ExeFilePath);

            File.WriteAllText(cmdPath, File.ReadAllText(cmdTemplatePath)
                .Replace("##EXEFILE##", ExeFilePath)
                .Replace("##MODELFILE##", ModelFilePath)
                .Replace("##INPUTFILE##", segFile)
                .Replace("##OUTPUTFILE##", outputFile));

            var lastOutputTime = DateTime.MinValue;
            if (File.Exists(outputFile))
            {
                lastOutputTime = (new FileInfo(outputFile)).CreationTime;
            }

            var arguments = string.Format("-test \"{0}\" -model \"{1}\" -output \"{2}\"", segFile, ModelFilePath, outputFile);
            var str = ProcessEx.Execute(ExeDir, exeFullPath, arguments);

            //var str = ProcessEx.Execute(ExeDir, cmdPath);

            LogHelper.Log(str);

            if (File.Exists(outputFile))
            {
                LogHelper.Log("Label output: " + outputFile);
                LogHelper.Enter();
                return true;
            }
            else
            {
                throw new Exception("Failed to label seg file.");
            }
        }
    }
}
