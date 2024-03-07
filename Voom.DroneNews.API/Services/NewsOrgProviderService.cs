using Newtonsoft.Json;
using System.Net.Http;
using Voom.DroneNews.API.Models;
using Voom.DroneNews.API.Services.Interfaces;

namespace Voom.DroneNews.API.Services
{
    public class NewsOrgProviderService : INewsProviderService
    {
        public List<Article> GetDronesNews()
        {
            var articles = new List<Article>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Voom.DronesNews.API/1.0");

                HttpResponseMessage response = client.GetAsync("https://newsapi.org/v2/everything?q=drones&apiKey=21e860b516094deb99822b18ea8ef1e1").Result;

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = response.Content.ReadAsStringAsync().Result;
                    dynamic data = JsonConvert.DeserializeObject(jsonResult);

                    foreach (var article in data.articles)
                    {
                        Article newsArticle = new Article
                        {
                            Title = article.title,
                            Description = article.description,
                            Url = article.url,
                            Author = article.author,
                            Content = article.content,
                            Source = article.source.name,
                            PublishedAt = article.publishedAt,  

                        };

                        articles.Add(newsArticle);
                    }
                }
                else
                {
                    string jsonResult = response.Content.ReadAsStringAsync().Result;
                    dynamic data = JsonConvert.DeserializeObject(jsonResult);
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }

            return articles;    
        }
    }
}
