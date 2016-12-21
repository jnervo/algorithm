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
        #region Merge

        public static void Convert2Crf(string labelInput, string segInput, string posInput, string outputDir, string fileName, bool need2Split, bool need2FilterFailedCase = false)
        {
            //read
            var labelingResults = File.ReadAllLines(labelInput).ToList();

            var segResults = ReadSegInputV1(segInput);

            var posInfoList = ReadPosInfoList(posInput);

            //convert
            var crfResults = Convert(labelingResults, segResults, posInfoList);

            //split & filter
            var attributesList = new List<List<string>>();
            var evaluationsList = new List<List<string>>();
            var expressionsList = new List<List<string>>();

            var labelingResults_filtered = new List<string>();
            var segResults_filtered = new List<List<string>>();
            var posResults_filtered = new List<PosInfo>();
            var crfResults_filtered = new List<CrfResult>();

            for (int i = 0; i < crfResults.Count; i++)
            {
                if (crfResults[i] == null)
                {
                    continue;
                }

                List<string> attributes, evaluations, expressions;

                DetectTags(crfResults[i], out attributes, out evaluations, out expressions);

                // if no need to filter failed case, or it has expressions result indeed, add it to output list
                if (!need2FilterFailedCase || expressions.Count > 0)
                {
                    attributesList.Add(attributes);
                    evaluationsList.Add(evaluations);
                    expressionsList.Add(expressions);

                    labelingResults_filtered.Add(labelingResults[i]);
                    segResults_filtered.Add(segResults[i]);
                    posResults_filtered.Add(posInfoList[i]);
                    crfResults_filtered.Add(crfResults[i]);
                }
            }

            OutputResults(outputDir, fileName,
                labelingResults_filtered,
                segResults_filtered,
                posResults_filtered,
                crfResults_filtered,
                attributesList,
                evaluationsList,
                expressionsList);
        }

        private static void OutputResults(string outputDir, string fileName,
            List<string> labelingResults,
            List<List<string>> segResults,
            List<PosInfo> posResults,
            List<CrfResult> crfResults,
            List<List<string>> attributesList,
            List<List<string>> evaluationsList,
            List<List<string>> expressionsList)
        {
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            //label file
            File.WriteAllLines(Path.Combine(outputDir, fileName + ".labeled"),
                labelingResults);

            //seg file
            File.WriteAllLines(Path.Combine(outputDir, fileName + ".seg"),
                segResults.Select(q => string.Join("\t", q)));

            //pos file
            File.WriteAllLines(Path.Combine(outputDir, fileName + ".pos"),
                posResults.Select(q => string.Join("\t", q.Words.Select(w => string.Join("/", w.Word, w.Pos)))));

            //crf file
            OutputCrfResults(Path.Combine(outputDir, fileName + ".crf"), crfResults);

            //split files: attributes, evalutions, expressions
            File.WriteAllLines(Path.Combine(outputDir, fileName + ".attributes"), attributesList.Select(q => string.Join("\t", q)));
            File.WriteAllLines(Path.Combine(outputDir, fileName + ".evaluations"), evaluationsList.Select(q => string.Join("\t", q)));
            File.WriteAllLines(Path.Combine(outputDir, fileName + ".expressions"), expressionsList.Select(q => string.Join("\t", q)));
        }

        public static void OutputCrfResults(string output, List<CrfResult> crfResults)
        {
            using (StreamWriter sw = new StreamWriter(output))
            {
                foreach (var crfResult in crfResults)
                {
                    foreach (var crfWord in crfResult.CrfWords)
                    {
                        sw.WriteLine(
                            string.Join("\t",
                                crfWord.WordStr,
                                crfWord.CrfPos,
                                crfWord.CrfCategoryPosition
                            ));
                    }
                    sw.WriteLine();
                }
            }
        }

        private static List<CrfResult> Convert(List<string> labelingResults, List<List<string>> segResults, List<PosInfo> posResults)
        {
            List<CrfResult> crfResultList = new List<CrfResult>();
            if (labelingResults == null
                || segResults == null
                || labelingResults.Count != segResults.Count
                || labelingResults.Count != posResults.Count)
            {
                return crfResultList;
            }

            for (int i = 0; i < labelingResults.Count; i++)
            {
                var crfResult = GenerateCrfResult(labelingResults[i], segResults[i]);
                if (crfResult != null)
                {
                    FillPosInfo(posResults[i], ref crfResult);
                }
                else
                {
                    Console.WriteLine("Failed to GenerateCrfResult for: {0}", labelingResults[i]);
                }
                crfResultList.Add(crfResult);
            }

            return crfResultList;
        }

        private static void FillPosInfo(PosInfo posInfo, ref CrfResult crfResult)
        {
            if (crfResult.CrfWords.Count != posInfo.Words.Count)
            {
                return;
            }
            for (int i = 0; i < crfResult.CrfWords.Count; i++)
            {
                crfResult.CrfWords[i].CrfPos = posInfo.Words[i].Pos;
            }
        }
        #endregion

        #region Labeled Result => CRF
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
        /// <param name="segInput">word breaker input</param>
        public static void ConvertLabel2Crf(string input, string output, string segInput)
        {
            var labelingResults = File.ReadAllLines(input).ToList();

            var segResults = ReadSegInputV1(segInput);

            var convertResults = Convert(labelingResults, segResults);

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

        private static List<CrfResult> Convert(List<string> labelingResults, List<List<string>> segResults)
        {
            List<CrfResult> crfResultList = new List<CrfResult>();
            if (labelingResults == null
                || segResults == null
                || labelingResults.Count != segResults.Count)
            {
                return crfResultList;
            }

            for (int i = 0; i < labelingResults.Count; i++)
            {
                var crfResult = GenerateCrfResult(labelingResults[i], segResults[i]);
                if (crfResult != null)
                {
                    crfResultList.Add(crfResult);
                }
            }

            return crfResultList;
        }

        private static CrfResult GenerateCrfResult(string labelingResult, List<string> words)
        {
            var subSentences = SplitLabelingResult(labelingResult);
            if (subSentences == null)
            {
                return null;
            }

            int wordIndex = 0;

            List<CrfWord> crfWords = new List<CrfWord>();

            foreach (var subSentence in subSentences)
            {
                var wordsInCurrentSubSentence = new List<string>();

                while (wordIndex < words.Count && string.Join("", wordsInCurrentSubSentence) != subSentence.WordStr
                    && string.Join("", wordsInCurrentSubSentence).Replace(" ", "") != subSentence.WordStr.Replace(" ", ""))
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
            try
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
            catch
            {
                Console.WriteLine("Invalid labeling result:{0}", labelingResult);
                return null;
            }
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

        /// <summary>
        /// the word breaker input is like:
        ///     屏幕
        ///     有点
        ///     大
        ///     ，
        /// </summary>
        /// <param name="segInput"></param>
        /// <returns></returns>
        private static List<List<string>> ReadSegInputV2(string segInput)
        {
            List<List<string>> sentences = new List<List<string>>();

            List<string> words = new List<string>();
            using (StreamReader sr = new StreamReader(segInput))
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

        /// <summary>
        /// the word breaker input is like:
        ///     有时	接	电话	会	黑屏	，	貌似	是	贴膜	把	距离	感应器	挡	到	了	，	可以	试	着	把	膜切	了	。
        /// </summary>
        /// <param name="segInput"></param>
        /// <returns></returns>
        private static List<List<string>> ReadSegInputV1(string segInput)
        {
            List<List<string>> sentences = new List<List<string>>();

            List<string> words = new List<string>();
            using (StreamReader sr = new StreamReader(segInput))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();

                    sentences.Add(line.Split('\t').ToList());
                }
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

            public CrfResult()
            {
                Words = new List<string>();
                CrfWords = new List<CrfWord>();
            }
        }

        public class CrfWord
        {
            public string WordStr;
            public string CrfPos;

            public CrfCategory CrfCategory;
            public CrfPosition CrfPosition;

            private string _crfCategoryPosition;
            public string CrfCategoryPosition
            {
                get
                {
                    if (string.IsNullOrEmpty(_crfCategoryPosition))
                    {
                        _crfCategoryPosition = this.CrfCategory == CrfCategory.O ?
                            "O" : string.Format("{0}-{1}", this.CrfPosition.ToString(), this.CrfCategory.ToString());
                    }
                    return _crfCategoryPosition;
                }
                set
                {
                    _crfCategoryPosition = value;
                }
            }
        }

        #endregion

        #region POS => CRF

        /// <summary>
        ///  the pos-input is like: 
        ///     屏幕/n	色彩/n	还原/v	准确/a	，/wp	让/v	人/n	看/v	得/u	很/d	舒服/a	，/wp	当然/d	这/r	与/p	incell/ws	技术/n	有着/v	密切/a	的/u	关系/n	。/wp
        ///     屏幕/n 大/a 带来/v 耗电量/n 较/d 高/a	，/wp 待机/n 时间/n 不/d 长/a	。/wp
        /// the output is like:
        ///     屏幕   NA   S-A
        ///     有点   NA   B-E
        ///     大   NA   E-E
        ///     ，   NA   O
        /// the labelInput input is like:
        ///     299	1	手感好，键盘舒服，外观喜欢，运行快。	手感	好	1	键盘	舒服	1	外观	喜欢	1	
        ///     300	1	拍照很不错，像素1200W差不多，实体拍照按键，但是好像自动聚焦做的不够完美。	拍照 像素1200W差不多，实体拍照按键	1	自动聚焦 做的不够完美	-1
        /// </summary>
        /// <param name="posInput"></param>
        /// <param name="output"></param>
        /// <param name="wordBreakerInput"></param>
        public static void ConvertPos2Crf(string posInput, string output, string labelInput)
        {
            //read
            List<PosInfo> posInfoList = ReadPosInfoList(posInput);

            List<CommentInfo> commentInfoList = ReadCommentInfoList(labelInput);

            //process
            List<CrfInfo> crfInfoList = GenerateCrfInfoList(posInfoList, commentInfoList);

            //output
            File.WriteAllLines(output, crfInfoList.Select(crfInfo => string.Join("\t", crfInfo.Word, crfInfo.Pos, crfInfo.Type)));
        }

        private static List<CrfInfo> GenerateCrfInfoList(List<PosInfo> posInfoList, List<CommentInfo> commentInfoList)
        {
            List<CrfInfo> list = new List<CrfInfo>();
            for (int i = 0; i < posInfoList.Count; i++)
            {
                GenerateCrfInfoList(posInfoList[i],
                    commentInfoList != null && commentInfoList.Count > i ? commentInfoList[i] : null,
                    ref list);
                list.Add(new CrfInfo());
            }
            return list;
        }

        private static void GenerateCrfInfoList(PosInfo posInfo, CommentInfo commentInfo, ref List<CrfInfo> list)
        {
            var wordList = new List<CrfInfo>();
            for (int i = 0; i < posInfo.Words.Count; i++)
            {
                CrfInfo crfInfo = new CrfInfo()
                {
                    Word = posInfo.Words[i].Word,
                    Pos = posInfo.Words[i].Pos
                };
                wordList.Add(crfInfo);
            }
            if (commentInfo != null)
            {
                for (int i = 0; i < wordList.Count; i++)
                {
                    var word = wordList[i].Word;

                    if (string.IsNullOrEmpty(word))
                    {
                        wordList[i].Type = "O";
                    }
                    else if (commentInfo.PropertyList.Any(p => string.Equals(p, word, StringComparison.OrdinalIgnoreCase)))
                    {
                        wordList[i].Type = "S-A";
                    }
                    else if (commentInfo.CommentList.Any(p => string.Equals(p, word, StringComparison.OrdinalIgnoreCase)))
                    {
                        wordList[i].Type = "S-E";
                    }
                    else if (commentInfo.PropertyList.Any(p => p.StartsWith(word, StringComparison.OrdinalIgnoreCase)))
                    {
                        bool matched = false;
                        string property = word;
                        for (int j = i + 1; j < wordList.Count; j++)
                        {
                            property += wordList[j].Word;
                            if (commentInfo.PropertyList.Any(p => string.Equals(p, property, StringComparison.OrdinalIgnoreCase)))
                            {
                                wordList[i].Type = "B-A";
                                for (int k = i + 1; k < j; k++)
                                {
                                    wordList[k].Type = "M-A";
                                }
                                wordList[j].Type = "E-A";
                                i = j;
                                matched = true;
                                break;
                            }
                        }
                        if (!matched)
                        {
                            wordList[i].Type = "O";
                        }
                    }
                    else if (commentInfo.CommentList.Any(p => p.StartsWith(word, StringComparison.OrdinalIgnoreCase)))
                    {
                        bool matched = false;
                        string commentStr = word;
                        for (int j = i + 1; j < wordList.Count; j++)
                        {
                            commentStr += wordList[j].Word;
                            if (commentInfo.CommentList.Any(p => string.Equals(p, commentStr, StringComparison.OrdinalIgnoreCase)))
                            {
                                wordList[i].Type = "B-E";
                                for (int k = i + 1; k < j; k++)
                                {
                                    wordList[k].Type = "M-E";
                                }
                                wordList[j].Type = "E-E";
                                i = j;
                                matched = true;
                                break;
                            }
                        }
                        if (!matched)
                        {
                            wordList[i].Type = "O";
                        }
                    }
                    else
                    {
                        wordList[i].Type = "O";
                    }
                }
            }
            list.AddRange(wordList);
        }

        private static List<CommentInfo> ReadCommentInfoList(string labelInput)
        {
            List<CommentInfo> list = new List<CommentInfo>();
            if (string.IsNullOrEmpty(labelInput) || !File.Exists(labelInput))
            {
                return list;
            }
            foreach (var line in File.ReadAllLines(labelInput))
            {
                CommentInfo commentInfo = new CommentInfo()
                {
                    Comments = new List<Comment>(),
                    PropertyList = new List<string>(),
                    CommentList = new List<string>()
                };

                var tokens = line.Split('\t');
                for (int i = 3; i + 2 < tokens.Length; i = i + 3)
                {
                    var comment = new Comment
                    {
                        Property = tokens[i],
                        CommentStr = tokens[i + 1],
                        Polarity = tokens[i + 2],
                    };

                    commentInfo.Comments.Add(comment);
                    commentInfo.PropertyList.Add(tokens[i]);
                    commentInfo.CommentList.Add(tokens[i + 1]);
                }
                list.Add(commentInfo);
            }
            return list;
        }

        private static List<PosInfo> ReadPosInfoList(string posInput)
        {
            List<PosInfo> list = new List<PosInfo>();

            foreach (var line in File.ReadAllLines(posInput))
            {
                PosInfo posInfo = new PosInfo()
                {
                    Words = new List<WordInfo>()
                };

                var words = line.Split('\t');
                foreach (string word in words)
                {
                    var tokens = word.Split('/');
                    if (tokens.Length != 2)
                    {
                    }
                    WordInfo wordInfo = new WordInfo()
                    {
                        Word = tokens[0],
                        Pos = tokens[1]
                    };
                    posInfo.Words.Add(wordInfo);
                }
                list.Add(posInfo);
            }
            return list;
        }

        public class CommentInfo
        {
            public List<Comment> Comments;
            public List<string> PropertyList;
            public List<string> CommentList;
        }

        public class Comment
        {
            public string Property;
            public string CommentStr;
            public string Polarity;
        }

        public class PosInfo
        {
            public List<WordInfo> Words;
        }

        public class WordInfo
        {
            public string Word;
            public string Pos;
        }

        public class CrfInfo
        {
            public string Word;
            public string Pos;
            public string Type;
        }

        #endregion

        #region CRF => Property + Evaluate + Expression

        public static void ConvertCrf2TagFiles(string crfFile)
        {
            var crfResults = ReadCrfResults(crfFile);

            var attributesList = new List<List<string>>();
            var evaluationsList = new List<List<string>>();
            var expressionsList = new List<List<string>>();

            var filteredCrfResults = new List<CrfResult>();

            foreach (var crfResult in crfResults)
            {
                List<string> attributes, evaluations, expressions;

                DetectTags(crfResult, out attributes, out evaluations, out expressions);

                if (expressions.Count > 0)
                {
                    attributesList.Add(attributes);
                    evaluationsList.Add(evaluations);
                    expressionsList.Add(expressions);

                    filteredCrfResults.Add(crfResult);
                }
            }

            OutputCrfResults(crfFile, filteredCrfResults);

            File.WriteAllLines(crfFile + ".attributes", attributesList.Select(q => string.Join("\t", q)));
            File.WriteAllLines(crfFile + ".evaluations", evaluationsList.Select(q => string.Join("\t", q)));
            File.WriteAllLines(crfFile + ".expressions", expressionsList.Select(q => string.Join("\t", q)));
        }

        private static void DetectTags(CrfResult crfResult, out List<string> attributes, out List<string> evaluations, out List<string> expressions)
        {
            attributes = new List<string>();
            evaluations = new List<string>();
            expressions = new List<string>();

            for (int i = 0; i < crfResult.CrfWords.Count; i++)
            {
                if (crfResult.CrfWords[i].CrfCategoryPosition == "O")
                {
                    continue;
                }
                var tokens = crfResult.CrfWords[i].CrfCategoryPosition.Split('-');
                var type = tokens.LastOrDefault();

                string fullWord = string.Empty;
                if (tokens.FirstOrDefault() == "S")
                {
                    fullWord = crfResult.CrfWords[i].WordStr;
                }
                else
                {
                    do
                    {
                        fullWord += crfResult.CrfWords[i].WordStr;
                    }
                    while (!crfResult.CrfWords[i++].CrfCategoryPosition.StartsWith("E-"));
                    i--;
                }

                switch (type)
                {
                    case "A": attributes.Add(fullWord); break;
                    case "E": evaluations.Add(fullWord); break;
                    case "Q": expressions.Add(fullWord); break;
                }
            }
        }

        private static List<CrfResult> ReadCrfResults(string crfFile)
        {
            var crfResults = new List<CrfResult>();
            CrfResult crfResult = new CrfResult();
            foreach (var line in File.ReadAllLines(crfFile))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    crfResults.Add(crfResult);
                    crfResult = new CrfResult();
                    continue;
                }

                var tokens = line.Split('\t');

                var crfWord = new CrfWord()
                {
                    WordStr = tokens[0],
                    CrfPos = tokens[1],
                    CrfCategoryPosition = tokens[2]
                };
                crfResult.CrfWords.Add(crfWord);
            }
            if (crfResult.CrfWords.Count > 0)
            {
                crfResults.Add(crfResult);
            }
            return crfResults;
        }

        #endregion

    }
}
