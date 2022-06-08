using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchKeywordsWtihLucene
{
    public class LuceneEngine
    {
        private Analyzer _Analyzer;
        private Lucene.Net.Store.Directory _Directory;
        private IndexWriter _IndexWriter;
        private IndexSearcher _IndexSearcher;
        private Document _Document;
        private QueryParser _QueryParser;
        private Query _Query;
        private string _IndexPath = @"C:\LuceneIndex";
        private Lucene.Net.Util.LuceneVersion lucVer = Lucene.Net.Util.LuceneVersion.LUCENE_30;
        public LuceneEngine()
        {
            _Analyzer = new StandardAnalyzer(lucVer);
            _Directory = FSDirectory.Open(_IndexPath);
        }

        public void AddToIndex(IEnumerable<Article> values)
        {

            var idxCfg = new IndexWriterConfig(lucVer, _Analyzer);

            using (_IndexWriter = new IndexWriter(_Directory, idxCfg))
            {
                foreach (var loopEntity in values)
                {
                    _Document = new Document();
                    foreach (var loopProperty in loopEntity.GetType().GetProperties())
                    {
                        // Dilersek indexlenmesini istemediğimiz bir alan varsa, kontrol ederek Field.Index.NO vermemiz yeterli olacaktır.
                        _Document.Add(new Field(loopProperty.Name, loopProperty.GetValue(loopEntity).ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    }
                    _IndexWriter.AddDocument(_Document);
                    _IndexWriter.Flush(triggerMerge: false, applyAllDeletes: false); 
                    _IndexWriter.Commit();
                    
                }
            }
        }

        public List<Article> Search(string field, string keyword)
        {
            /*
             * Üzerinde arama yapmak istediğimiz field için bir query oluşturuyoruz.
             */
            var idxCfg = new IndexWriterConfig(lucVer, _Analyzer);
            _IndexWriter = new IndexWriter(_Directory, idxCfg);
            _QueryParser = new QueryParser(lucVer, field, _Analyzer);
            _Query = _QueryParser.Parse(keyword);
            var reader = _IndexWriter.GetReader(true);
            _IndexSearcher = new IndexSearcher(reader);

            List<Article> news = new List<Article>();
            var result = _IndexSearcher.Search(_Query, 10);

            foreach (var loopDoc in result.ScoreDocs.OrderBy(s => s.Score))
            {
                _Document = _IndexSearcher.Doc(loopDoc.Doc);
                news.Add(new Article() { Id = Convert.ToInt32(_Document.Get("Id")), Title = _Document.Get("Title"), Content = _Document.Get("Content") });
            }
            return news;

        }
    }
}
