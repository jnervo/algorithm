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
            StanfordNlpParserResultHandler.FormatConvert("out.txt", "out.convert1", "fenci.out");
        }
    }
}
