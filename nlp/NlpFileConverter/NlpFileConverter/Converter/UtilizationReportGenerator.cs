using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NlpFileConverter
{
    class UtilizationReportGenerator
    {
        /// <summary>
        /// parse html: https://apportal.osdinfra.net/Utilization?group=BingEXp&team=STC-Beijing&feature=News
        /// </summary>
        public static void UtilizationReport()
        {
            var content = File.ReadAllText(@"D:\Segmentation\tools\HtmlAnalyzer\cpu_raw.html");

            //Regex regex = new Regex("\\<div class=\\\"odd ap-util-entityMetrics\\\" .*?\\>(?<EnvironmentName>.*?)\\<\\/span\\>\\<\\/a\\>\\<div class=\\\"ap-util-entityMetrics-content\\\" .*?\\>(?<Metric>.*?)\\<\\/span\\>\\<\\/div\\>\\<div class=\\\"ap-stackedBar horizontal\\\" .*?\\>(?<ColdNumber>.*?)\\<\\/span\\>\\<span class=\\\"half-width-col\\\" .*?\\>(?<ColdRate>.*?\\%)\\<\\/span\\>\\<\\/span\\>\\<\\/div\\>\\<div .*?class=\\\"ap-stackedBar-item cool\\\""); 

            //MatchCollection matches = regex.Matches(content);

            //foreach (Match match in matches)
            //{
            //    var EnvironmentName = match.Groups["EnvironmentName"].Value;
            //}
        }
    }
}
