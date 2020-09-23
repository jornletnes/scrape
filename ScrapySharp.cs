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
        private static int[] _numPagesAtLevel;
        private static int _depth = 10;

        public static void Run()
        {
            //_browser.UseDefaultCookiesParser = false;
            _browser.IgnoreCookies = true;

            _numLinksAtLevel = new int[_depth];
            _numPagesAtLevel = new int[_depth];


            //var mainPageLinks = GetMainPageLinks("https://p4d.developer.delfi.cloud.slb-ds.com/");
            //PrintLinks("https://p4d.developer.delfi.cloud.slb-ds.com/solutions/dataecosystem/apis/search-service", 10);
            //PrintLinks("https://www.software.slb.com/", 0); // 100
            PrintLinks("https://db.no/", 0); // 10

            PrintWidth();
        }

        private static void PrintWidth()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("How wide have you jumped?");
            Console.WriteLine("");

            for (int i = 0; i < _depth; ++i)
            {
                Console.WriteLine("I found " + _numLinksAtLevel[i] + " links in " + _numPagesAtLevel[i] + " pages.");
            }
        }

        private static void PrintLinks(string url, int numLevel)
        {
            if (numLevel >= _depth)
                return;

            _numPagesAtLevel[numLevel] += 1;

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
                    //Console.WriteLine(currentLink);
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
