using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OldBookSearch.Services.Book
{
    [DataContract]
    public class HanmotoDataRoot
    {
        [DataMember(Name = "onix")]
        public HanmotoData HanmotoData { get; set; }

        [DataMember(Name = "summary")]
        public Summary Summary { get; set; }
    }

    #region onix

    [DataContract]
    public class HanmotoData
    {

        [DataMember(Name = "ProductSupply")]
        public ProductSupply ProductSupply { get; set; }
    }

    #endregion

    #region PublishingDetail

    [DataContract]
    public class ProductSupply
    {
        [DataMember(Name = "SupplyDetail")]
        public SupplyDetail SupplyDetail { get; set; }

    }

    [DataContract]
    public class SupplyDetail
    {
        [DataMember(Name = "Price")]
        public List<Price> Price { get; set; }
    }

    [DataContract]
    public class Price
    {
        [DataMember(Name = "PriceType")]
        public string PriceType { get; set; }

        [DataMember(Name = "PriceAmount")]
        public int PriceAmount { get; set; }

        [DataMember(Name = "CurrencyCode")]
        public string CurrencyCode { get; set; }
    }

    #endregion

    #region summary

    [DataContract]
    public class Summary
    {
        [DataMember(Name = "publisher")]
        public string Publisher { get; set; }

        [DataMember(Name = "pubdate")]
        public string Pubdate { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "author")]
        public string Author { get; set; }

        [DataMember(Name = "cover")]
        public string Cover { get; set; }
    }

    #endregion
}
