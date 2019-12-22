using AngleSharp.Html.Parser;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OldBookSearch.Services.Product
{
    public class BookOffService : FetchClient, IProductGet
    {
        private const string bookoffURLBase = "https://www.bookoffonline.co.jp/display/L001,st=u,bg=12,q={0}";
        private const string productURLBase = "https://www.bookoffonline.co.jp";
        public BookOffService(IJSRuntime jSRuntime) : base(jSRuntime)
        {
        }
        public async Task<ProductInfo> ProductGetAsync(string isbn)
        {
            var html = await GetHtmlAsync(string.Format(bookoffURLBase, isbn), true);
            return await GetProductInfoFromHtml(html);
        }

        private async Task<ProductInfo> GetProductInfoFromHtml(string html)
        {
            var pinfo = new ProductInfo();
            pinfo.StoreName = "BookOffオンライン";

            var parser = new HtmlParser();
            var htmlDocument = await parser.ParseDocumentAsync(html);

            // get price
            var price = htmlDocument.QuerySelector("td.mainprice");
            if (price != null)
            {
                var priceStr = price.TextContent.Replace("￥", "").Replace(",", "").Split("（税込）")[0].Trim();
                pinfo.Price = int.Parse(priceStr);
                pinfo.IsNotSoldOut = true;

                var link = htmlDocument.QuerySelector("div.list_l a");
                if (link != null)
                {
                    var linkStr = link.GetAttribute("href");
                    pinfo.LinkUrl = productURLBase + linkStr;
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
