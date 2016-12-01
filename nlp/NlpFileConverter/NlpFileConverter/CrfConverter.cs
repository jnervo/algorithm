using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NlpFileConverter
{
    public class CrfConverter
    {
        /// <summary>
        /// the input is like: 
        ///     <a>屏幕</a><e>有点大</e>，<exp-3>5.0的屏就能满足要求了</exp-3>
        /// the output is like:
        ///     屏幕   NA   S-A
        ///     有点   NA   B-E
        ///     大   NA   E-E
        ///     ，   NA   O
        /// the word breaker input is like:
        ///     屏幕
        ///     有点
        ///     大
        ///     ，
        /// </summary>
        /// <param name="input">the input file of corpus labeling result</param>
        /// <param name="output">CRF output</param>
        /// <param name="wordBreakerInput">word breaker input</param>
        public static void Convert(string input, string output, string wordBreakerInput)
        {
            var labelingResults = File.ReadAllLines(input).ToList();

            var wordBreakerResults = ReadWordBreakerResults(wordBreakerInput);

            var convertResults = Convert(labelingResults, wordBreakerResults);

            using (StreamWriter sw = new StreamWriter(output))
            {
                foreach (var crfResult in convertResults)
                {
                    foreach (var crfWord in crfResult.CrfWords)
                    {
                        sw.WriteLine(
                            string.Join("\t",
                                crfWord.WordStr,
                                "NA",
                                crfWord.CrfCategory == CrfCategory.O ? "O" : string.Format("{0}-{1}", crfWord.CrfPosition.ToString(), crfWord.CrfCategory.ToString())
                            ));
                    }
                    sw.WriteLine();
                }
            }
        }

        private static List<CrfResult> Convert(List<string> labelingResults, List<List<string>> wordBreakerResults)
        {
            List<CrfResult> crfResultList = new List<CrfResult>();
            if (labelingResults == null
                || wordBreakerResults == null
                || labelingResults.Count != wordBreakerResults.Count)
            {
                return crfResultList;
            }

            for (int i = 0; i < labelingResults.Count; i++)
            {
                var crfResult = GenerateCrfResult(labelingResults[i], wordBreakerResults[i]);
                crfResultList.Add(crfResult);
            }

            return crfResultList;
        }

        private static CrfResult GenerateCrfResult(string labelingResult, List<string> words)
        {
            var subSentences = SplitLabelingResult(labelingResult);

            int wordIndex = 0;


            List<CrfWord> crfWords = new List<CrfWord>();

            foreach (var subSentence in subSentences)
            {
                var wordsInCurrentSubSentence = new List<string>();

                while (wordIndex < words.Count && string.Join("", wordsInCurrentSubSentence) != subSentence.WordStr)
                {
                    wordsInCurrentSubSentence.Add(words[wordIndex]);
                    wordIndex++;
                }

                if (wordsInCurrentSubSentence.Count > 0)
                {
                    if (wordsInCurrentSubSentence.Count == 1)
                    {
                        crfWords.Add(new CrfWord()
                        {
                            WordStr = wordsInCurrentSubSentence[0],
                            CrfCategory = subSentence.CrfCategory,
                            CrfPosition = CrfPosition.S
                        });
                    }
                    else
                    {
                        for (int i = 0; i < wordsInCurrentSubSentence.Count; i++)
                        {
                            crfWords.Add(new CrfWord()
                            {
                                WordStr = wordsInCurrentSubSentence[i],
                                CrfCategory = subSentence.CrfCategory,
                                CrfPosition = i == 0 ? CrfPosition.B : i == wordsInCurrentSubSentence.Count - 1 ? CrfPosition.E : CrfPosition.M
                            });
                        }
                    }
                }
            }

            CrfResult crfResult = new CrfResult()
            {
                Words = words,
                CrfWords = crfWords
            };
            return crfResult;
        }

        /// <summary>
        /// input:
        ///     <a>屏幕</a><e>有点大</e>，<exp-3>5.0的屏就能满足要求了</exp-3>
        /// output:
        ///     屏幕  A
        ///     有点大 E
        ///     5.0的屏就能满足要求了    O
        /// </summary>
        /// <param name="labelingResult"></param>
        /// <returns></returns>
        public static List<CrfWord> SplitLabelingResult(string labelingResult)
        {
            List<CrfWord> splitResult = new List<CrfWord>();

            int openTagLength = "<a>".Length;
            int closeTagLength = "</a>".Length;

            int index = 0;
            int nextOpenTag = labelingResult.IndexOf("<");

            string word = string.Empty;
            while (nextOpenTag != -1)
            {
                if (nextOpenTag > index)
                {
                    word = labelingResult.Substring(index, nextOpenTag - index);
                    splitResult.Add(new CrfWord()
                    {
                        WordStr = word,
                        CrfCategory = CrfCategory.O
                    });
                }

                var openTag = labelingResult.Substring(nextOpenTag, labelingResult.IndexOf(">", nextOpenTag) + 1 - nextOpenTag);
                var tagCategory = GetCrfCategory(openTag);

                var nextCloseTag = labelingResult.IndexOf("</", nextOpenTag);
                var closeTag = labelingResult.Substring(nextCloseTag, labelingResult.IndexOf(">", nextCloseTag) + 1 - nextCloseTag);

                var tagValue = labelingResult.Substring(nextOpenTag + openTag.Length, nextCloseTag - nextOpenTag - openTag.Length);

                splitResult.Add(new CrfWord()
                {
                    WordStr = tagValue,
                    CrfCategory = tagCategory
                });

                nextOpenTag = labelingResult.IndexOf("<", nextCloseTag + 1);
                index = nextCloseTag + closeTag.Length;
            }
            if (index < labelingResult.Length)
            {
                word = labelingResult.Substring(index, labelingResult.Length - index);
                splitResult.Add(new CrfWord()
                {
                    WordStr = word,
                    CrfCategory = CrfCategory.O
                });
            }

            return splitResult;
        }

        private static CrfCategory GetCrfCategory(string categoryTag)
        {
            switch (categoryTag.ToLower())
            {
                case "<a>": return CrfCategory.A;
                case "<e>": return CrfCategory.E;
                default: return CrfCategory.Q;
            }
        }

        private static List<List<string>> ReadWordBreakerResults(string wordBreakerInput)
        {
            List<List<string>> sentences = new List<List<string>>();

            List<string> words = new List<string>();
            using (StreamReader sr = new StreamReader(wordBreakerInput))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();

                    if (string.IsNullOrEmpty(line) && words.Count > 0)
                    {
                        sentences.Add(words);
                        words = new List<string>();
                    }
                    else
                    {
                        words.Add(line);
                    }
                }
            }
            if (words.Count > 0)
            {
                sentences.Add(words);
            }
            return sentences;
        }

        public enum CrfCategory
        {
            A,
            E,
            Q,
            O
        }
        public enum CrfPosition
        {
            S,
            B,
            M,
            E,
            O
        }

        public class CrfResult
        {
            public List<string> Words;
            public List<CrfWord> CrfWords;
        }

        public class CrfWord
        {
            public string WordStr;
            public CrfCategory CrfCategory;
            public CrfPosition CrfPosition;
        }

    }
}
