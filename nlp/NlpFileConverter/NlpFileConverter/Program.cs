using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NlpFileConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            CrfConverter.Convert(@".\Data\CRF\input.txt", @".\Data\CRF\out.txt", @".\Data\CRF\wordBreakerInput.txt");

            //StanfordNlpParserResultConverter.FormatConvert(@".\Data\Stanford\out.txt", @".\Data\Stanford\out.convert", @".\Data\Stanford\fenci.out");
        }
    }
}
