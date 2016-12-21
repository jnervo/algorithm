using Parsers.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsers
{
    class Program
    {
        static void Main(string[] args)
        {
            MitParser.Parse();
            UtilizationReportParser.Parse();
        }
    }
}
