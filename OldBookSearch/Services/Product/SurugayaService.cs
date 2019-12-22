using AngleSharp.Html.Parser;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OldBookSearch.Services.Product
{
    public class SurugayaService : FetchClient, IProductGet
    {
        private const string baseUrl = "https://www.suruga-ya.jp/search?category=&search_word={0}&inStock=On";

        private const string productUrlBase = "https://www.suruga-ya.jp";
        public SurugayaService(IJSRuntime jSRuntime) : base(jSRuntime)
        {
        }
        public async Task<ProductInfo> ProductGetAsync(string isbn)
        {
            try
            {
                var url = string.Format(baseUrl, isbn);
                var html = await GetHtmlAsync(url);

                return await GetProductInfoFromHtml(html);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e);
                throw e;
            }
        }

        private async Task<ProductInfo> GetProductInfoFromHtml(string html)
        {
            // Console.WriteLine("StartHTML");
            // Console.WriteLine(html);
            var pinfo = new ProductInfo();
            pinfo.StoreName = "駿河屋";

            var parser = new HtmlParser();
            var htmlDocument = await parser.ParseDocumentAsync(html);

            // get price
            var price = htmlDocument.QuerySelector("div.item_price p.price_teika span.text-red strong");
            if (price != null)
            {
                var priceStr = price.TextContent.Replace("￥", "").Replace(",", "").Trim();
                pinfo.Price = int.Parse(priceStr);
                pinfo.IsNotSoldOut = true;
                var link = htmlDocument.QuerySelector("p.title a");
                if (link != null)
                {
                    pinfo.LinkUrl = productUrlBase + link.GetAttribute("href");
                    // Console.WriteLine(pinfo.LinkUrl);
                }
            }
            else
            {
                pinfo.IsNotSoldOut = false;
            }
            return pinfo;
        }
    }
}
