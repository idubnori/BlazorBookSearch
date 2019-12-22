using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OldBookSearch.Services.Product
{
    public interface IProductServiceFactory
    {
        IProductGet GetProducService(StoreType storeType);
    }

    public enum StoreType
    {
        AmazonMarket,
        Surugaya,
        Bookoff
    }
}
