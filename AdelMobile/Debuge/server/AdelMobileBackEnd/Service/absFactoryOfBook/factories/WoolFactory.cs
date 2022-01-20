using AdelMobileBackEnd.models.absFactoryOfBook.products;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdelMobileBackEnd.models.absFactoryOfBook.factories
{
    public class WoolFactory : FactoryOfBook
    {
        public async override Task<absBook> GetBook()
        {
         
                using (HttpClientHandler handler = new HttpClientHandler { AllowAutoRedirect = false, AutomaticDecompression = DecompressionMethods.All })
                {

                    using (HttpClient client = new HttpClient(handler))
                    {
                        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                        using (HttpResponseMessage response = await client.GetAsync("https://ficbook.net/readfic/10463740"))
                        {
                            var ficbook = await response.Content.ReadAsStringAsync();
                            if (string.IsNullOrEmpty(ficbook))
                                return null;
                            HtmlDocument doc = new HtmlDocument();
                            doc.LoadHtml(ficbook);

                            var likeHtml = doc.DocumentNode.SelectSingleNode(".//span[@class='badge-text js-marks-plus']").InnerText;
                            var titleHtml = doc.DocumentNode.SelectSingleNode(".//h1[@class='mb-10']").InnerText;
                            ficbook = ficbook.Replace("\n", " ").Replace(" ", "");
                            int start = ficbook.IndexOf(@"/icons/icons-sprite5.svg#ic_bubble-dark");
                            ficbook = ficbook.Substring(start);
                            start = ficbook.IndexOf(@"</svg>");
                            ficbook = ficbook.Substring(start);
                            int end = ficbook.IndexOf("</span>");
                            var commentsHtml = ficbook.Substring(0, end).Replace("</svg>","") ; 
                            return new Wool(titleHtml, int.Parse(commentsHtml), int.Parse(likeHtml));
                        }
                    }
                }
            }
          
    }
}
