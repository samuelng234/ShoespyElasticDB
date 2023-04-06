using NikeProductPolling.Repositories;
using NikeProductPollingES.Models.DBDocuments;
using NikeProductPollingES.Models.Polling;

namespace NikeProductPolling.Services
{
    internal class ProductService
    {
        private ProductRepository repository;

        internal ProductService() 
        {
            repository = new ProductRepository();
        }

        internal async void AddNikeProducts(IEnumerable<NikeProduct> products)
        {
            var elasticProducts = MapProductIntoElasticModel(products);
            
            //await UpsertProducts(elasticProducts);

            // disable inactive products
        }

        //private async Task<IEnumerable<Product>> UpsertProducts(IEnumerable<Product> products)
        //{
        //    var bulkUpsert = new List<WriteModel<Product>>();
        //    var pids = new List<string>();

        //    foreach (var product in products)
        //    {
        //        pids.AddRange(product.Colorways.Select(x => x.Pid));

        //        FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.AnyIn(x => x.PIds, product.Colorways.Select(x => x.Pid));
        //        UpdateDefinition<Product> update = Builders<Product>.Update
        //            .Set("PIds", product.Colorways.Select(x => x.Pid))
        //            .Set("Name", product.Title)
        //            .Set("Category", product.Subtitle)
        //            .Set("Active", true)
        //            .Set("WebsiteId", websiteId);

        //        var upsertOne = new UpdateOneModel<Product>(filterDefinition, update) { IsUpsert = true };
        //        bulkUpsert.Add(upsertOne);
        //    }

        //    await repository.UpsertProducts(bulkUpsert);
        //    return await repository.GetProducts(Builders<Product>.Filter.AnyIn(x => x.PIds, pids));
        //}

        private IEnumerable<Product> MapProductIntoElasticModel(IEnumerable<NikeProduct> products)
        {
            var result = new List<Product>();

            foreach (var product in products)
            {
                var productToAdd = new Product()
                {
                    WebsiteName = "Nike",
                    WebsiteBaseUrl = "https://www.nike.com/nz/",
                    CountryId = "NZ",
                    Active = true
                };

                foreach (var colourway in product.Colorways)
                {
                    // colourway data
                    productToAdd.PId = colourway.Pid;
                    productToAdd.Guid = colourway.CloudProductId;
                    productToAdd.Url = colourway.PdpUrl;
                    productToAdd.Description = colourway.ColorDescription;
                    productToAdd.InStock = colourway.InStock;
                    productToAdd.IsComingSoon = colourway.IsComingSoon;
                    productToAdd.IsNew = colourway.IsNew;

                    // price data
                    productToAdd.Currency = colourway.Price.Currency;
                    productToAdd.CurrentPrice = colourway.Price.CurrentPrice;
                    productToAdd.FullPrice = colourway.Price.FullPrice;
                    productToAdd.Discounted  = colourway.Price.Discounted;
                }

                result.Add(productToAdd);
            }

            return result;
        }
    }
}
