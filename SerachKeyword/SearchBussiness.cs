
using SearchKeywordsWtihLucene;
using SerachKeyword.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerachKeyword
{
    public class SearchBussiness
    {
        public bool SearchWithContains(string text, List<string> keywords)
        {
            return  keywords.Contains(text);
        }

        public List<Article> SearchWithLucene(List<string> keywords)
        {
            LuceneEngine luceneEngine = new LuceneEngine();

            List<Article> articles = new List<Article>();
            articles.Add(new Article() { Id = 1, Title = "After the crypto crash, here’s what industry experts are waiting for next", Content = @"Cryptocurrency companies dominated the main street at the World Economic Forum in Davos this year, a notable difference between this edition and the last one in 2020.
               The high - profile presence from the industry came even as the cryptocurrency market crashed.It was sparked by the collapse of the so - called algorithmic stablecoin called terraUSD or UST,
                which saw its sister token luna drop to $0 in May.Meanwhile,
                global regulators are setting their sights on the cryptocurrency industry.WEF is the annual gathering of global business leaders and politicians that aims to set the agenda for the year.Against that backdrop, it was the perfect time to catch up with some of the big players in the cryptocurrency industry.Here’s what I learned." });

            articles.Add(new Article() { Id = 2, Title = "First Mover Asia: Bitcoin Climbs Past $30K, but Bearish Sentiment Remains", Content = @"The largest crypto by market capitalization was recently trading at about $30,600, a more than 2% gain over the previous 24 hours. Ether was changing hands at just above $1,800, up slightly for the same period. Most other major cryptos rose late with ADA recently rising nearly 6%, and APE up over 4%. Trading was choppy as investors continued to shy away from riskier assets – their behavior a product of inflationary and recessionary fears that have mushroomed steadily this year." });
            articles.Add(new Article() { Id = 3, Title = "Kanye West Files Trademarks Describing NFT Technology After Denouncing the Digital Collectible Concept", Content = @"." });
            articles.Add(new Article() { Id = 4, Title = "Anonymous Hacks Major Belarusian Government Websites", Content = @"The websites of several ministries of Belarus have allegedly been taken down in a new attack, part of the cyberwar Anonymous is waging to help Ukraine. The hacking group declared it’s targeting the Belarusian government for its complicity in the Russian invasion of the neighboring country." });


            luceneEngine.AddToIndex(articles);

            List<Article> results = new List<Article>();
            foreach(string keyword in keywords)
                 results.AddRange(luceneEngine.Search("Content",keyword));

            return results;
        }

        public void SearchWithElastic()
        {
            List<Articles> articles = new List<Articles>();
            articles.Add(new Articles() { Id = 1, Title = "After the crypto crash, here’s what industry experts are waiting for next", Content = @"Cryptocurrency companies dominated the main street at the World Economic Forum in Davos this year, a notable difference between this edition and the last one in 2020.
               The high - profile presence from the industry came even as the cryptocurrency market crashed.It was sparked by the collapse of the so - called algorithmic stablecoin called terraUSD or UST,
                which saw its sister token luna drop to $0 in May.Meanwhile,
                global regulators are setting their sights on the cryptocurrency industry.WEF is the annual gathering of global business leaders and politicians that aims to set the agenda for the year.Against that backdrop, it was the perfect time to catch up with some of the big players in the cryptocurrency industry.Here’s what I learned." });

            articles.Add(new Articles() { Id = 2, Title = "First Mover Asia: Bitcoin Climbs Past $30K, but Bearish Sentiment Remains", Content = @"The largest crypto by market capitalization was recently trading at about $30,600, a more than 2% gain over the previous 24 hours. Ether was changing hands at just above $1,800, up slightly for the same period. Most other major cryptos rose late with ADA recently rising nearly 6%, and APE up over 4%. Trading was choppy as investors continued to shy away from riskier assets – their behavior a product of inflationary and recessionary fears that have mushroomed steadily this year." });
            articles.Add(new Articles() { Id = 3, Title = "Kanye West Files Trademarks Describing NFT Technology After Denouncing the Digital Collectible Concept", Content = @"." });
            articles.Add(new Articles() { Id = 4, Title = "Anonymous Hacks Major Belarusian Government Websites", Content = @"The websites of several ministries of Belarus have allegedly been taken down in a new attack, part of the cyberwar Anonymous is waging to help Ukraine. The hacking group declared it’s targeting the Belarusian government for its complicity in the Russian invasion of the neighboring country." });

            ElasticSearchService elasticSearch = new ElasticSearchService();
            //elasticSearch.AddIndex(articles);
            elasticSearch.SearchwithKeyword("Belarus");
        }
    }
}
