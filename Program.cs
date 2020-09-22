using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace scrape
{
    class Program
    {
        static ScrapingBrowser _browser = new ScrapingBrowser();
        static List<string> _visitedLinks = new List<string>();

        static void Main(string[] args)
        {
            ScrapySharpMain();
        }

        private static void ScrapySharpMain()
        {
            //_browser.UseDefaultCookiesParser = false;
            _browser.IgnoreCookies = true;

            int numLevels = 150;
            //var mainPageLinks = GetMainPageLinks("https://p4d.developer.delfi.cloud.slb-ds.com/");
            PrintLinks("https://www.software.slb.com/", numLevels); // 150
                                                                    //PrintLinks("https://db.no/", numLevels); //10
        }

        static void PrintLinks(string url, int numLevels)
        {
            if (numLevels <= 0)
                return;

            try
            {
                var html = GetHtml(url);
                var links = html?.CssSelect("a");

                foreach (var link in links)
                {
                    var currentLink = link.Attributes["href"].Value;
                    if (_visitedLinks.Contains(currentLink))
                        continue;

                    _visitedLinks.Add(currentLink);
                    Console.WriteLine(currentLink);
                    PrintLinks(currentLink, --numLevels);
                }

            }
            catch (UriFormatException) { }
            catch (Exception e)
            {
                Console.WriteLine("I failed a bit: " + e.Message);
            }

        }

        static HtmlNode GetHtml(string url)
        {
            WebPage webpage = _browser.NavigateToPage(new Uri(url));
            return webpage?.Html;
        }
    }
}
