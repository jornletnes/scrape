using System.Collections.Specialized;
using Scraping.Web;

namespace scrape
{
    class ScrapingToolkit
    {
        public void Run()
        {
            Run("https://github.com/otavioalfenas/Scraping-Toolkit");
        }

        private void Run(string url)
        {
            var ret = new HttpRequestFluent(true);
            ret.OnLoad += Ret_OnLoad;
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("Name", "Value");

            ret.FromUrl(url)
                .TryGetComponents(Enums.TypeComponent.ComboBox | Enums.TypeComponent.DataGrid |
                                Enums.TypeComponent.Image | Enums.TypeComponent.InputCheckbox |
                                Enums.TypeComponent.InputHidden | Enums.TypeComponent.InputText |
                                Enums.TypeComponent.LinkButton)
                .RemoveHeader("name")
                .AddHeader("name", "value")
                .KeepAlive(true)
                .WithAccept("Accept")
                .WithAcceptEncoding("Accept-Encoding")
                .WithAcceptLanguage("Accept-Language")
                .WithAutoRedirect(true)
                .WithContentType("ContentType")
                .WithMaxRedirect(2)
                .WithParameters(parameters)
                .WithPreAuthenticate(true)
                .WithReferer("Referer")
                .WithRequestedWith("WithRequestedWidth")
                .WithTimeoutRequest(100)
                .WithUserAgent("User-Agent")
            .Load();
        }

        private void Ret_OnLoad(object sender, RequestHttpEventArgs e)
        {
            string page = e.HtmlPage;
            ResponseHttp response = e.ResponseHttp;
        }
    }
}
