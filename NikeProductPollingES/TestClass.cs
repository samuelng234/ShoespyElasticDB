using Elasticsearch.Net;
using Nest;
using NikeProductPollingES.Models.DBDocuments;

namespace NikeProductPollingES
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    internal class TestClass
    {
        public TestClass() 
        {
            var node = new Uri("https://localhost:9200");
            var settings = new ConnectionSettings(node);
            settings.ThrowExceptions(alwaysThrow: true); // I like exceptions
            settings.PrettyJson(); // Good for DEBUG
            settings.EnableApiVersioningHeader();
            settings.CertificateFingerprint("94:75:CE:4F:EB:05:32:83:40:B8:18:BB:79:01:7B:E0:F0:B6:C3:01:57:DB:4D:F5:D8:B8:A6:BA:BD:6D:C5:C4");
            settings.BasicAuthentication("shoespy", "shoespy");
            var client = new ElasticClient(settings);

            //client.Update<Test>(id: 1, b => b.Upsert(new Test()
            //{
            //    Id = 1,
            //    Name = "test"
            //}));


            var products = new List<Test>()
            {
                new Test()
                {
                    Id = 1,
                    Name = "test"
                },
                new Test()
                {
                    Id = 2,
                    Name = "test"
                }
            };

            var bulkUpdates = new BulkDescriptor();
            foreach (var product in products)
            {
                bulkUpdates.Update<Test>(b => b.Upsert(product));
            }

            client.Bulk(u =>
                bulkUpdates
            );
        }
    }
}
