using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace scrape
{
    class ScrapySharp
    {
        private static ScrapingBrowser _browser = new ScrapingBrowser();
        private static List<string> _visitedLinks = new List<string>();
        private static int[] _numLinksAtLevel;
        private static int _depth = 100;

        public static void Run()
        {
            //_browser.UseDefaultCookiesParser = false;
            _browser.IgnoreCookies = true;

            _numLinksAtLevel = new int[_depth];


            //var mainPageLinks = GetMainPageLinks("https://p4d.developer.delfi.cloud.slb-ds.com/");
            //PrintLinks("https://p4d.developer.delfi.cloud.slb-ds.com/solutions/dataecosystem/apis/search-service", 10);
            PrintLinks("https://www.software.slb.com/", 0); // 100
            //PrintLinks("https://db.no/", 0); // 10

            PrintWidth();
        }

        private static void PrintWidth()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("How wide have you jumped?");
            Console.WriteLine("");

            foreach (int level in _numLinksAtLevel)
            {
                Console.WriteLine(level);
            }
        }

        private static void PrintLinks(string url, int numLevel)
        {
            if (numLevel >= _depth)
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
                    _numLinksAtLevel[numLevel] += 1;
                    PrintLinks(currentLink, numLevel + 1);
                }

            }
            catch (UriFormatException) { }
            catch (Exception e)
            {
                Console.WriteLine("I failed a bit: " + e.Message);
            }

        }

        private static HtmlNode GetHtml(string url)
        {
            WebPage webpage = _browser.NavigateToPage(new Uri(url));
            return webpage?.Html;
        }
    }
}
