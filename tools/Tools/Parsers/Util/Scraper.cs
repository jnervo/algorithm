using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
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
        public static string QuickDownloadUrlContent(string queryUrl, Dictionary<string, string> headers = null, WebProxy wp = null)
        {
            try
            {
                WebClient client = new WebClient();

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        client.Headers.Add(header.Key, header.Value);
                    }
                }

                if (wp != null)
                {
                    client.Proxy = wp;
                }

                Stream data = client.OpenRead(queryUrl);

                if (client.ResponseHeaders["Content-Encoding"] == "gzip")
                {
                    GZipStream g = new GZipStream(data, CompressionMode.Decompress);
                    byte[] d = new byte[20480];
                    int l = g.Read(d, 0, 20480);
                    StringBuilder stringBuilder = new StringBuilder(102400);
                    while (l > 0)
                    {
                        stringBuilder.Append(Encoding.Default.GetString(d, 0, l));
                        l = g.Read(d, 0, 20480);
                    }

                    return stringBuilder.ToString();
                }

                StreamReader reader = new StreamReader(data);
                string result = reader.ReadToEnd();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }
    }


}
