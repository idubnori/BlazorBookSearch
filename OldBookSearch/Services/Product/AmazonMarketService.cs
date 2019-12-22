using AngleSharp.Html.Parser;
using Microsoft.JSInterop;
using OldBookSearch.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OldBookSearch.Services.Product
{
    public class AmazonMarketService : FetchClient, IProductGet
    {
        private const string amazonURLBase = "https://www.amazon.co.jp/gp/offer-listing/";

        public AmazonMarketService(IJSRuntime jSRuntime) : base(jSRuntime)
        {
        }
        public async Task<ProductInfo> ProductGetAsync(string isbn)
        {
            var isbn10 = IsbnConverter.Convert13To10(isbn);
            var html = await GetHtmlAsync(amazonURLBase + isbn10);
            Console.WriteLine("Success:" + html);
            return await GetProductInfoFromHtml(html, isbn10);
        }

        private async Task<ProductInfo> GetProductInfoFromHtml(string html, string isbn10)
        {
            var pinfo = new ProductInfo();
            pinfo.StoreName = "Amazonマケプレ";

            var parser = new HtmlParser();
            var htmlDocument = await parser.ParseDocumentAsync(html);

            // get price
            var price = htmlDocument.QuerySelector("span.olpOfferPrice");
            if (price != null)
            {
                var priceStr = price.TextContent.Replace("￥", "").Replace(",", "").Trim();
                pinfo.Price = int.Parse(priceStr);

                var shipping = htmlDocument.QuerySelector("span.olpShippingPrice");
                if (shipping != null)
                {
                    pinfo.Shipping = int.Parse(shipping.TextContent.Replace("￥", "").Replace(",", "").Trim());
                }

                pinfo.IsNotSoldOut = true;
                pinfo.LinkUrl = amazonURLBase + isbn10;
            }
            else
            {
                pinfo.IsNotSoldOut = false;
            }

            return pinfo;
        }
    }
}
