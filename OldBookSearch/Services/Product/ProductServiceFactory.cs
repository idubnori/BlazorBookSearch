using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OldBookSearch.Services.Product
{
    public class ProductServiceFactory : IProductServiceFactory
    {
        private IJSRuntime _JSRuntime;
        public ProductServiceFactory(IJSRuntime jSRuntime)
        {
            _JSRuntime = jSRuntime;
        }
        public IProductGet GetProducService(StoreType storeType)
        {
            switch (storeType)
            {
                case StoreType.AmazonMarket:
                    return new AmazonMarketService(_JSRuntime);
                case StoreType.Surugaya:
                    return new SurugayaService(_JSRuntime);
                case StoreType.Bookoff:
                    return new BookOffService(_JSRuntime);
                default:
                    throw new ArgumentException($"not supported store type:{storeType}");
            }
        }
    }
}
