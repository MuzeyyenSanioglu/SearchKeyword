// See https://aka.ms/new-console-template for more information
using SerachKeyword;

Console.WriteLine("Hello, World!");
//List<string> keywords = new List<string>();
//keywords.Add("Anonymous");
//keywords.Add("crypto");
//keywords.Add("Bearish");
//keywords.Add("Kanye West");
SearchBussiness searchBussiness = new SearchBussiness();
//searchBussiness.SearchWithLucene(keywords);
searchBussiness.SearchWithElastic();

