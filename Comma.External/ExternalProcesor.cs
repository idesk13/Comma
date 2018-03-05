using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Comma.External
{
    public class ExternalProcesor
    {
        private static HtmlDocument ParseHtml(string verb)
        {
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            var html = webClient.DownloadString("http://conjugare.ro/romana/conjugarea-verbului-" + verb);
        
            var document = new HtmlDocument();
 
            document.LoadHtml(html);

            return document;
        }

        public List<TimpVerbal> GetIndicativPrezent(string verb)
        {
            var doc = ParseHtml(verb);
            var list = new List<TimpVerbal>();
            var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class,'box_conj')]");

            foreach (var node in nodes)
            {
                var timpVerbalNume = node.SelectSingleNode(".//b").InnerText;
                TimpVerbal timpVerbal= new TimpVerbal(timpVerbalNume);
                list.Add(timpVerbal);
                foreach (var conjValue in node.SelectNodes(".//div[contains(@class,'cont_conj')]").ToList())
                {
                    var pronumNode = conjValue.SelectSingleNode(".//i");
                    var pronumValue = string.Empty;

                    if (pronumNode != null)
                    {
                        pronumValue = pronumNode.InnerText;
                    }

                    var verbz = conjValue.SelectSingleNode("./text()").InnerText;

                    if (pronumValue.StartsWith("eu"))
                    {
                        timpVerbal.Eu = verbz;
                        timpVerbal.EuPronume = pronumValue;
                    }

                    if (pronumValue.StartsWith("tu"))
                    {
                        timpVerbal.Tu = verbz;
                        timpVerbal.TuPronume = pronumValue;
                    }

                    if (pronumValue.StartsWith("el/ea"))
                    {
                        timpVerbal.Ea = verbz;
                        timpVerbal.EaPronume = pronumValue;
                    }

                    if (pronumValue.StartsWith("noi"))
                    {
                        timpVerbal.Noi = verbz;
                        timpVerbal.NoiPronume = pronumValue;
                    }

                    if (pronumValue.StartsWith("voi"))
                    {
                        timpVerbal.Voi = verbz;
                        timpVerbal.VoiPronume = pronumValue;
                    }

                    if (pronumValue.StartsWith("ei/ele"))
                    {
                        timpVerbal.Ele = verbz;
                        timpVerbal.ElePronume = pronumValue;
                    }
                }

               
            }
            return list;
        }

    }
}