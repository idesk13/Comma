using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Comma.External
{
    public class ExternalProcesor
    {
        private static HtmlDocument ParseHtml(string verb)
        {
            var webClient = new WebClient();
            var html = webClient.DownloadString("http://conjugare.ro/romana/conjugarea-verbului-" + verb);
            var document = new HtmlDocument();
            document.LoadHtml(html);

            return document;
        }

        public string GetIndicativPrezent(string verb)
        {
            var doc = ParseHtml(verb);

            var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class,'box_conj')]");

            foreach (var node in nodes)
            {
                var conjName = node.SelectSingleNode(".//b").InnerText;


                foreach (var conjValue in node.SelectNodes(".//div[contains(@class,'cont_conj')]").ToList())
                {
                    var pronumNode = conjValue.SelectSingleNode(".//i");
                    var pronumValue = string.Empty;

                    if (pronumNode != null)
                    {
                        pronumValue = pronumNode.InnerText;
                    }

                    var verbz = conjValue.SelectSingleNode("./text()").InnerText;

                    MessageBox.Show("Pronume: " + pronumValue + " Verb: " + verbz);
                }

            }

            return string.Empty;
        }

        //static void Main(string[] args)
        //{
        //    var doc = ParseHtml();

        //    var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class,'cont_conj')]");

        //    foreach (var node in nodes)
        //    {
        //    }
        //}
    }
}
