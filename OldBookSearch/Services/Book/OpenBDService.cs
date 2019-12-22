using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using System.Runtime.Serialization.Json;

namespace OldBookSearch.Services.Book
{
    public class OpenBDService : IBookGet
    {
        private const string baseUrl = @"https://api.openbd.jp/v1/get?isbn={0}";

        private readonly HttpClient _httpClient;

        public OpenBDService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<BookInfo> GetBookInfoAsync(string isbn13)
        {
            var url = string.Format(baseUrl, isbn13);
            var bdInfo = await _httpClient.GetAsync(url);
            if (bdInfo.IsSuccessStatusCode)
            {
                var serializer = new DataContractJsonSerializer(typeof(List<HanmotoDataRoot>));
                var infoList = (List<HanmotoDataRoot>)serializer.ReadObject(await bdInfo.Content.ReadAsStreamAsync());

                if (infoList.Count > 0)
                {
                    var bookInfo = new BookInfo();
                    bookInfo.BookAuthor = infoList[0].Summary?.Author;
                    bookInfo.BookTitle = infoList[0].Summary?.Title;
                    bookInfo.BookImageUrl = infoList[0].Summary?.Cover;
                    bookInfo.PubDate = infoList[0].Summary?.Pubdate;
                    bookInfo.Isbn13 = isbn13;
                    try
                    {
                        if (infoList[0].HanmotoData?.ProductSupply?.SupplyDetail?.Price[0]?.CurrencyCode == "JPY")
                        {
                            bookInfo.BookPrice = infoList[0].HanmotoData.ProductSupply.SupplyDetail.Price[0].PriceAmount;
                        }
                    }
                    catch (Exception) { }

                    return bookInfo;
                }
            }

            return null;
        }
    }
}
