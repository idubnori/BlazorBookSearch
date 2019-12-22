using OldBookSearch.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OldBookSearch.Utility
{
    public static class StoreTypeExtension
    {
        public static string GetStoreTypeString(this StoreType storeType)
        {
            switch (storeType)
            {
                case StoreType.AmazonMarket:
                    return "Amazonマケプレ";
                case StoreType.Surugaya:
                    return "駿河屋";
                case StoreType.Bookoff:
                    return "BookOffオンライン";
                default:
                    return "不明";
            }
        }
    }
}
