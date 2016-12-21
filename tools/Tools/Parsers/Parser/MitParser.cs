using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Parsers.Parser
{
    public class MitParser
    {
        /// <summary>
        /// this is for Grammy/Oscar carousel topic feed
        /// </summary>
        /// <param name="xmlFile"></param>
        public static void Parse(string xmlFile = @"D:\yuzhu\src\git\github\algorithm\tools\Tools\Parsers\Data\2017_grammy_mainline_nominees.xml")
        {
            var artistList = new List<string>();
            XDocument doc = XDocument.Load(xmlFile);
            foreach (var item in doc.Descendants("EntertainmentAwards.Context"))
            {
                var mentionType = item.Element("MentionType").Value;
                if (mentionType == "Artist")
                {
                    var artist = item.Element("Url").Element("Text").Value;

                    artistList.Add(artist);
                }
            }
            File.WriteAllLines(xmlFile.Replace(".xml", ".txt"), artistList.Distinct());
        }
    }
}
