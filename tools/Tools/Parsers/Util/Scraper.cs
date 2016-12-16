using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsers
{
    public class Scraper
    {
        public static void RenderUrl(string url, string fileName, string outputDir, int delay = 10000, int timeout = 30000)
        {
            if (Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            var arguments = string.Format(" -u \"{0}\" -f \"{1}\" -d \"{2}\" -dl {3} -t {4} -mh 0", url, fileName, outputDir, delay, timeout);

            var programDir = @"D:\Segmentation\tools\HtmlAnalyzer\";
            var programPath = Path.Combine(programDir, "RealtimeSegmentation.HtmlAnalyzer.exe");

            var str = ProcessEx.Execute(programDir, programPath, arguments);
        }

    }


}
