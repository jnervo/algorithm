using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Parsers.Parser
{
    class UtilizationReportParser
    {
        /// <summary>
        /// parse html: https://apportal.osdinfra.net/Utilization?group=BingEXp&team=STC-Beijing&feature=News
        /// </summary>
        public static void Parse()
        {
            var url = "https://apportal.osdinfra.net/Utilization?group=BingEXp&team=STC-Beijing&feature=News&hidememory=false";

            var fileName = "pivot";
            var fileDir = @"D:\yuzhu\src\git\github\tools\Tools\Parsers\bin\Debug\Utilizaiton";

            var file = Path.Combine(fileDir, fileName + ".html");
            if (!File.Exists(file))
            {
                Scraper.RenderUrl(url, fileName, fileDir);
            }

            if (!File.Exists(file))
            {
                Console.WriteLine("{0} is not found.", file);
                return;
            }

            List<UtilizationStatistic> list = ParseUtilization(file);

            foreach (var environment in list)
            {
                fileName = environment.name.Replace("(", "_").Replace(")", "_").Trim('_');

                file = Path.Combine(fileDir, fileName + ".html");
                if (!File.Exists(file))
                {
                    Scraper.RenderUrl(environment.detailUrl, fileName, fileDir, 20000);
                }
                if (!File.Exists(file))
                {
                    Console.WriteLine("{0} is not found.", file);
                    continue;
                }
                environment.details = ParseUtilization(file);
            }

            using (StreamWriter sw = new StreamWriter(Path.Combine(fileDir, "raw.txt")))
            {
                var environments = list.GroupBy(l => l.name);
                foreach (var env in environments)
                {
                    foreach (var metric in env)
                    {
                        sw.WriteLine(string.Join("\t",
                                    metric.name,
                                    "",
                                    metric.metric,
                                    metric.totalNum,
                                    metric.coldNum,
                                    metric.coldRate,
                                    metric.coolNum,
                                    metric.coolRate,
                                    metric.warmNum,
                                    metric.warmRate,
                                    metric.mediumNum,
                                    metric.mediumRate,
                                    metric.hotNum,
                                    metric.hotRate));
                    }

                    foreach (var mf in env.FirstOrDefault().details)
                    {
                        sw.WriteLine(string.Join("\t",
                                    "",
                                    mf.name,
                                    mf.metric,
                                    mf.totalNum,
                                    mf.coldNum,
                                    mf.coldRate,
                                    mf.coolNum,
                                    mf.coolRate,
                                    mf.warmNum,
                                    mf.warmRate,
                                    mf.mediumNum,
                                    mf.mediumRate,
                                    mf.hotNum,
                                    mf.hotRate));
                    }
                }
            }
            //File.WriteAllLines(@"memory_raw.tsv", list.Select(l => s
        }

        private static List<UtilizationStatistic> ParseUtilization(string file)
        {
            List<UtilizationStatistic> list = new List<UtilizationStatistic>();

            var content = File.ReadAllText(file);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(content);

            var nodes = doc.DocumentNode.SelectNodes("//div[@class='odd ap-util-entityMetrics' or @class='even ap-util-entityMetrics']");
            foreach (var node in nodes)
            {
                var environments = ParseUtilization(node);
                list.AddRange(environments);
            }

            return list;

        }
        private static List<UtilizationStatistic> ParseUtilization(HtmlNode node)
        {
            List<UtilizationStatistic> list = new List<UtilizationStatistic>();

            var name = node.SelectSingleNode("a[@class='ap-util-entityMetrics-title']").InnerText;
            var detailUrl = node.SelectSingleNode("a[@class='ap-util-entityMetrics-title']").Attributes["href"].Value.Replace("&amp;", "&");
            var metrics = node.SelectNodes(".//div[@class='ap-util-singleMetric']");

            foreach (var metric in metrics)
            {
                var statistic = new UtilizationStatistic()
                {
                    name = name,
                    detailUrl = detailUrl,
                    metric = metric.SelectSingleNode(".//div[@class='ap-util-singleMetric-metric']").InnerText,
                    coldNum = Convert.ToInt32(metric.SelectSingleNode(".//div[@class='ap-stackedBar-item cold']/span/span[1]").InnerText),
                    coldRate = Convert.ToDouble(metric.SelectSingleNode(".//div[@class='ap-stackedBar-item cold']/span/span[2]").InnerText.TrimEnd('%')) / 100.0,
                    coolNum = Convert.ToInt32(metric.SelectSingleNode(".//div[@class='ap-stackedBar-item cool']/span/span[1]").InnerText),
                    coolRate = Convert.ToDouble(metric.SelectSingleNode(".//div[@class='ap-stackedBar-item cool']/span/span[2]").InnerText.TrimEnd('%')) / 100.0,
                    warmNum = Convert.ToInt32(metric.SelectSingleNode(".//div[@class='ap-stackedBar-item warm']/span/span[1]").InnerText),
                    warmRate = Convert.ToDouble(metric.SelectSingleNode(".//div[@class='ap-stackedBar-item warm']/span/span[2]").InnerText.TrimEnd('%')) / 100.0,
                    mediumNum = Convert.ToInt32(metric.SelectSingleNode(".//div[@class='ap-stackedBar-item medium']/span/span[1]").InnerText),
                    mediumRate = Convert.ToDouble(metric.SelectSingleNode(".//div[@class='ap-stackedBar-item medium']/span/span[2]").InnerText.TrimEnd('%')) / 100.0,
                    hotNum = Convert.ToInt32(metric.SelectSingleNode(".//div[@class='ap-stackedBar-item hot']/span/span[1]").InnerText),
                    hotRate = Convert.ToDouble(metric.SelectSingleNode(".//div[@class='ap-stackedBar-item hot']/span/span[2]").InnerText.TrimEnd('%')) / 100.0,

                };

                var cells = metric.SelectNodes(".//div[@class='ap-stackedBar-item cell']/span/span[1]");
                if (cells.Count == 2)
                {
                    statistic.notAssignedNum = Convert.ToInt32(cells[0].InnerText);
                    statistic.totalNum = Convert.ToInt32(cells[1].InnerText);
                }
                else if (cells.Count == 1)
                {
                    statistic.totalNum = Convert.ToInt32(cells[0].InnerText);
                }
                list.Add(statistic);
            }
            return list;
        }

        public class UtilizationStatistic
        {
            public string name;
            public string detailUrl;
            public string metric;

            public int coldNum;
            public double coldRate;
            public int coolNum;
            public double coolRate;
            public int warmNum;
            public double warmRate;
            public int mediumNum;
            public double mediumRate;
            public int hotNum;
            public double hotRate;
            public int notAssignedNum;
            public int totalNum;
            public List<UtilizationStatistic> details;
        }
    }
}
