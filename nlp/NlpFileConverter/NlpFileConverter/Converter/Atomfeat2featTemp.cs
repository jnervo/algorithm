using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NlpFileConverter
{
    public class Atomfeat2featTemp
    {
        public void ConvertFeature(string inputFile, string tempFile, string outputFile)
        {
            if (string.IsNullOrEmpty(outputFile))
            {
                outputFile = Path.ChangeExtension(inputFile, ".featTemp");
            }

            List<string> outputLines = new List<string>();

            List<List<KeyValuePair<int, int>>> v_tmp = new List<List<KeyValuePair<int, int>>>();

            foreach (var line in File.ReadAllLines(tempFile))
            {
                List<KeyValuePair<int, int>> l_t = new List<KeyValuePair<int, int>>();
                var data = line.Split(' ');
                for (int i = 0; i < data.Length; i++)
                {
                    int star = data[i].IndexOf("[");
                    int mid = data[i].IndexOf(",");
                    int end = data[i].IndexOf("]");
                    int idx0 = Convert.ToInt32(data[i].Substring(star + 1, mid - star - 1));
                    int idx1 = Convert.ToInt32(data[i].Substring(mid + 1, end - mid - 1));

                    l_t.Add(new KeyValuePair<int, int>(idx0, idx1));
                }
                v_tmp.Add(l_t);
            }

            List<List<string>> v_sent = new List<List<string>>();
            var lines = File.ReadAllLines(inputFile);
            foreach (var line in lines)
            {
                var data = line.Split('\t');
                List<string> tuple = new List<string>();
                if (data.Length >= 2)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        tuple.Add(data[i]);
                    }
                    v_sent.Add(tuple);
                }
                else
                {
                    string tmp = "";
                    for (int i = 0; i < v_sent.Count; i++)
                    {
                        tmp += v_sent[i][0] + " ";
                        for (int j = 0; j < v_tmp.Count; j++)
                        {
                            tmp += "[S" + j.ToString() + "]";
                            for (int k = 0; k < v_tmp[j].Count; k++)
                            {
                                if (k > 0)
                                {
                                    tmp += "#";
                                }
                                int idx0 = v_tmp[j][k].Key;
                                int idx1 = v_tmp[j][k].Value;
                                int row = i + idx0;
                                int cow = idx1;
                                if (row < 0)
                                {
                                    if (cow == 0)
                                    {
                                        tmp += "<s>";
                                    }
                                    else
                                    {
                                        tmp += "null";
                                    }
                                }
                                else if (row >= v_sent.Count)
                                {
                                    if (cow == 0)
                                    {
                                        tmp += "</s>";
                                    }
                                    else
                                    {
                                        tmp += "null";
                                    }
                                }
                                else
                                {
                                    tmp += v_sent[row][cow];
                                }
                            }
                            tmp += " ";
                        }

                        tmp += v_sent[i][v_sent[i].Count - 1];
                        tmp += "\n";
                    }
                    outputLines.Add(tmp);
                    v_sent = new List<List<string>>();
                }
            }
            File.WriteAllLines(outputFile, outputLines);
        }
    }
}
