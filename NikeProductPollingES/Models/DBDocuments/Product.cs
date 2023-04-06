namespace NikeProductPollingES.Models.DBDocuments
{
    internal class Product
    {
        public string PId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool Active { get; set; }
        public string Gender { get; set; }

        // brand data
        public string BrandName { get; set; }
        public string Logo { get; set; }

        // colourway data
        public string Sku { get; set; }
        public string Guid { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool InStock { get; set; }
        public bool IsComingSoon { get; set; }
        public bool IsNew { get; set; }
        public IEnumerable<string> Images { get; set; }

        // price data
        public string Currency { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal FullPrice { get; set; }
        public bool? Discounted { get; set; }

        // website data
        public string WebsiteName { get; set; }
        public string CountryId { get; set; }
        public string WebsiteBaseUrl { get; set; }
    }
}
