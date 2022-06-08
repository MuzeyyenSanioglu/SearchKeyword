using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerachKeyword.ElasticSearch
{
    public class ElasticSearchService
    {
        public static ElasticClient EsClient()
        {
            ConnectionSettings connectionSettings;
            ElasticClient elasticClient;
            connectionSettings = new ConnectionSettings(new Uri("http://localhost:9200/"));
            elasticClient = new ElasticClient(connectionSettings);
            return elasticClient;
        }
        public void AddIndex(IEnumerable<Articles> values)
        {
                
            ElasticClient client = EsClient();

            client.Indices.Create("news",
                index => index.Map<Articles>(
                    x => x.AutoMap()
                    ));
            var result = client.IndexMany(values,"news");
        }

        public void SearchGetAllElastic()
        {
            var res = EsClient().Search<Articles>(s => s.Index("news").From(0).Size(1000).Query( q => q.MatchAll()));
        }
        public void SearchwithKeyword(string keyword)
        {
            var serachResponse = EsClient().Search<Articles>(s => s.Index("news").From(0).Size(1000).Query( p => 
            p.Match( m=> m
            .Field(f => f.Content)
            .Query(keyword))));
        }
    }
}
