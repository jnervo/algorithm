﻿using NLPIntegratedTool.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NLPIntegratedTool.LstmProcessor;

namespace NLPIntegratedTool
{
    public class Processor
    {
        private static string DefaultDataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Exe", "DefaultData");

        private static string ResultDataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ResultData");

        public static string DefaultDataFilePath
        {
            get
            {
                switch (LstmProcessor.CurrentModelType)
                {
                    case ModelType.Mobile:
                        return Path.Combine(DefaultDataDir, "mobile.txt");
                    case ModelType.Hotel:
                        return Path.Combine(DefaultDataDir, "hotel.txt");
                    default:
                        return Path.Combine(DefaultDataDir, "model");
                }
            }
        }

        public static string GetTempInputFile()
        {
            var dir = Path.Combine(ResultDataDir, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            return Path.Combine(dir, "input.txt");
        }

        internal static ProcessResult ProcessText(string text)
        {
            var result = new ProcessResult();
            File.WriteAllText(result.InputFile, text);

            LogHelper.Log("#### Step 1 : input => seg ####");
            NlpProcessor.Seg(result);
            LogHelper.Log("#### Step 2 : seg => pos ####");
            NlpProcessor.Pos(result);
            LogHelper.Log("#### Step 3 : skip ####");
            LogHelper.Enter();
            LogHelper.Log("#### Step 4 : pos => crf ####");
            CrfProcessor.Pos2Crf(result);
            LogHelper.Log("#### Step 5 : skip ####");
            LogHelper.Log("#### Step 6 : skip ####");
            LogHelper.Enter();
            LogHelper.Log("#### Step 7 : crf => lstm ####");
            CrfProcessor.Crf2Lstm(result);
            LogHelper.Log("#### Step 8 : lstm => final output ####");
            LstmProcessor.Lstm2FinalOutput(result);
            LogHelper.Log("#### Finish!!! ####");
            result.Read();

            return result;
        }
    }

    public class ProcessResult
    {
        public string InputFile;
        public List<Sentence> Sentences;

        public ProcessResult()
        {
            InputFile = Processor.GetTempInputFile();
        }

        public string SegFile
        {
            get
            {
                return Path.ChangeExtension(InputFile, ".seg");
            }
        }

        public string PosFile
        {
            get
            {
                return Path.ChangeExtension(InputFile, ".pos");
            }
        }

        public string PosCrfFile
        {
            get
            {
                return Path.ChangeExtension(InputFile, ".poscrf");
            }
        }
        public string LstmFile
        {
            get
            {
                return Path.ChangeExtension(InputFile, ".lstm");
            }
        }
        public string OutputFile
        {
            get
            {
                return Path.ChangeExtension(InputFile, ".out");
            }
        }

        public bool Read()
        {
            Sentences = File.ReadAllLines(InputFile).Select(l => new Sentence()
            {
                SentenceStr = l
            }).ToList();

            var finalResults = NlpFileConverter.CrfConverter.ReadCrfResults(OutputFile);

            if (finalResults == null || finalResults.Count != Sentences.Count)
            {
                throw new Exception("Failed to read final result. Please check: " + OutputFile);
            }

            for (int i = 0; i < Sentences.Count; i++)
            {
                List<string> attributes, evaluations, expressions;

                NlpFileConverter.CrfConverter.DetectTags(finalResults[i], out attributes, out evaluations, out expressions);

                Sentences[i].Attributes = attributes;
                Sentences[i].Evaluations = evaluations;
                Sentences[i].Expressions = expressions;
            }
            return true;
        }
    }

    public class Sentence
    {
        public string SentenceStr;
        public List<string> Attributes;
        public List<string> Evaluations;
        public List<string> Expressions;
    }
}
