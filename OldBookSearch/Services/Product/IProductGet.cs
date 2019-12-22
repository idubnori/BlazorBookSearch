using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OldBookSearch.Services.Product
{
    public interface IProductGet
    {
        Task<ProductInfo> ProductGetAsync(string isbn);
    }

    public abstract class FetchClient
    {
        private readonly IJSRuntime _JSRuntime;
        public FetchClient(IJSRuntime jSRuntime)
        {
            _JSRuntime = jSRuntime;
        }

        protected virtual async Task<string> GetHtmlAsync(string url, bool isEncodeSJis = false)
        {
            return await _JSRuntime.InvokeAsync<string>("fetchHttp.getHtmlAsync", new object[] { url, isEncodeSJis }).ConfigureAwait(false);
        }
    }

    public class ProductInfo
    {
        public ProductInfo()
        {
            IsNotSoldOut = false;
            Shipping = -1;
        }

        public string StoreName { get; set; }
        public string ProductName { get; set; }

        public string LinkUrl { get; set; }

        public int Price { get; set; }

        public int Shipping { get; set; }

        public bool IsNotSoldOut { get; set; }
    }
}
