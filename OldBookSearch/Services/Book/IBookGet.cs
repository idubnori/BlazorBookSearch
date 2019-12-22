using OldBookSearch.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OldBookSearch.Services.Book
{
    interface IBookGet
    {
        Task<BookInfo> GetBookInfoAsync(string isbn13);
    }

    public class BookInfo
    {
        public string Isbn13 { get; set; }

        public string BookTitle { get; set; }

        public string BookAuthor { get; set; }

        public string PubDate { get; set; }

        public string BookImageUrl { get; set; }

        public int BookPrice { get; set; }

        public string AmazonLink
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Isbn13))
                {
                    var isbn10 = IsbnConverter.Convert13To10(Isbn13);
                    return $"https://www.amazon.co.jp/dp/{isbn10}";
                }
                else
                {
                    return string.Empty;
                }

            }
        }
    }

}
