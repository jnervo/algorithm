using Parsers.Helper;
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
            RelatedEntityHelper.Statistic();
            ImageVerticalParser.GetThumbnailIdsForAllTopics();
            MitParser.Parse();
            UtilizationReportParser.Parse();
        }
    }
}
