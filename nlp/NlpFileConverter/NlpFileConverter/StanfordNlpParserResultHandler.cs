
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NlpFileConverter
{
    //http://nlp.stanford.edu/software/lex-parser.html
    /*
    （1）nsubj(形容词,  名词) ，cop（形容词，系动词）将名词标记为 cnn，将形容词标记为 cadj
    （2）amod(名词，形容词)。将名词标记为 npnn，将形容词标记为 npadj
    （3）nn(governor，dependent)，代表名词词组中单词间的关系，其中 dependent 用来修饰 governor。将其中 governor 的句法语义结构特征标记同时作为 dependent 的句法语义结构特征标记
    （4）conj(governor，dependent)，代表并列形容词或名词。将其中 governor 的句法语义结构特征标记同时作为 dependent 的句法语义结构特征标记
    （5）neg（形容词，否定词）如果已知形容词被标记为npadj或cadj，且存在否定词修饰形容词，则重新将该形容词标记为npadjneg或cadjneg
    （6）advmod(形容词，程度副词)如果已知形容词被标记为 npadj 或 npadjneg，则标记程度副词的句法语义结构特征为 npadv；如果已知形容词被标记为 cadj或 cadjneg，则标记程度副词的句法语义结构特征为 cadv
    （7）除上述情况，都标为n
    比如说:
    det(手感-3, 全-1)
    nn(手感-3, 键盘-2)
    nn(输入-5, 手感-3)
    amod(输入-5, 不错-4)
    nsubj(方便-7, 输入-5)
    advmod(方便-7, 很-6)
    root(ROOT-0, 方便-7)
    变成这样:
    全 n
    键盘 cnn
    手感 cnn
    不错 npadj
    输入 cnn
    很 cadv
    方便 cadj
*/
    class StanfordNlpParserResultHandler
    {
        public static void FormatConvert(string input, string output)
        {
            var sentenceList = ReadSentenceList(input);

            AdaptSentenceList(sentenceList);

            WriteSentenceList(output, sentenceList);
        }

        private static void WriteSentenceList(string output, List<Sentence> sentenceList)
        {
            using (StreamWriter sw = new StreamWriter(output))
            {
                foreach (var sentence in sentenceList)
                {
                    foreach (var word in sentence.words)
                    {
                        sw.WriteLine(string.Join("\t", word.str, word.tag));
                    }
                    sw.WriteLine();
                }
            }
        }

        private static void AdaptSentenceList(List<Sentence> sentenceList)
        {
            sentenceList.ForEach(s => AdaptSentence(s));
        }

        private static void AdaptSentence(Sentence sentence)
        {
            var wordsDic = AdaptWords(sentence);

            foreach (var relation in sentence.relations.Where(r => r.type == "nsubj"))
            {
                wordsDic[relation.word1.position].tag = "cadj";
                wordsDic[relation.word2.position].tag = "cnn";
            }
            foreach (var relation in sentence.relations.Where(r => r.type == "cop"))
            {
                wordsDic[relation.word1.position].tag = "cadj";
            }
            foreach (var relation in sentence.relations.Where(r => r.type == "amod"))
            {
                if (wordsDic[relation.word1.position].tag == "n")
                {
                    wordsDic[relation.word1.position].tag = "npnn";
                }
                if (wordsDic[relation.word2.position].tag == "n")
                {
                    wordsDic[relation.word2.position].tag = "npadj";
                }
            }
            foreach (var relation in sentence.relations.Where(r => r.type == "nn"))
            {
                recursion(relation, sentence, wordsDic, "nn");
            }
            foreach (var relation in sentence.relations.Where(r => r.type == "conj"))
            {
                recursion(relation, sentence, wordsDic, "conj");
            }
            foreach (var relation in sentence.relations.Where(r => r.type == "neg"))
            {
                if (wordsDic[relation.word1.position].tag == "npadj")
                {
                    wordsDic[relation.word1.position].tag = "npadjneg";
                }
                else if (wordsDic[relation.word1.position].tag == "cadj")
                {
                    wordsDic[relation.word1.position].tag = "cadjneg";
                }
            }
            foreach (var relation in sentence.relations.Where(r => r.type == "advmod"))
            {
                if (wordsDic[relation.word1.position].tag == "npadj" || wordsDic[relation.word1.position].tag == "npadjneg")
                {
                    wordsDic[relation.word2.position].tag = "npadv";
                }
                if (wordsDic[relation.word1.position].tag == "cadj" || wordsDic[relation.word1.position].tag == "cadjneg")
                {
                    wordsDic[relation.word2.position].tag = "cadv";
                }
            }
            sentence.words = wordsDic.Where(w => w.Key != 0).OrderBy(w => w.Key).Select(w => w.Value).ToList();
        }

        private static void recursion(Relation relation, Sentence sentence, Dictionary<int, Word> wordsDic, string type)
        {
            if (wordsDic[relation.word1.position].tag != "n")
            {
                wordsDic[relation.word2.position].tag = wordsDic[relation.word1.position].tag;

                foreach (var depAsGover in sentence.relations.Where(r =>
                    r.word1.position == relation.word2.position
                    && r.type == type))
                {
                    recursion(depAsGover, sentence, wordsDic, type);
                }
            }
        }

        private static Dictionary<int, Word> AdaptWords(Sentence sentence)
        {
            Dictionary<int, Word> dic = new Dictionary<int, Word>();
            foreach (var relation in sentence.relations)
            {
                if (!dic.ContainsKey(relation.word1.position))
                {
                    dic[relation.word1.position] = relation.word1;
                }
                if (!dic.ContainsKey(relation.word2.position))
                {
                    dic[relation.word2.position] = relation.word2;
                }
            }
            return dic;
        }

        private static List<Sentence> ReadSentenceList(string input)
        {
            List<Sentence> sentenceList = new List<Sentence>();
            var sentence = new Sentence();
            using (StreamReader sr = new StreamReader(input))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        if (sentence.relations.Count > 0)
                        {
                            sentenceList.Add(sentence);
                        }
                        sentence = new Sentence();
                        continue;
                    }

                    var relation = ParseRelation(line);
                    if (relation != null)
                    {
                        sentence.relations.Add(relation);
                    }
                }
            }
            if (sentence.relations.Count > 0)
            {
                sentenceList.Add(sentence);
            }
            return sentenceList;
        }

        private static Regex regex = new Regex("(?<Relation>[^\\(\\)].*?)\\((?<Word1>.*?)-(?<Position1>\\d*?), (?<Word2>.*?)-(?<Position2>\\d*?)\\)");

        private static Relation ParseRelation(string line)
        {
            Match match = regex.Match(line);

            if (match.Success)
            {
                return new Relation()
                {
                    type = match.Groups["Relation"].Value,
                    word1 = new Word()
                    {
                        str = match.Groups["Word1"].Value,
                        position = Convert.ToInt32(match.Groups["Position1"].Value),
                        tag = "n"
                    },
                    word2 = new Word()
                    {
                        str = match.Groups["Word2"].Value,
                        position = Convert.ToInt32(match.Groups["Position2"].Value),
                        tag = "n"
                    },
                };
            }
            return null;
        }
    }
    public class Relation
    {
        public string type;
        public Word word1;
        public Word word2;
    }

    public class Sentence
    {
        public List<Word> words;
        public List<Relation> relations;

        public Sentence()
        {
            words = new List<Word>();
            relations = new List<Relation>();
        }
    }

    public class Word
    {
        public string str;
        public int position;
        public string tag;
    }
}
