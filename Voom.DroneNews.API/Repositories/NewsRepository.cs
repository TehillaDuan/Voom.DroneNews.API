using Voom.DroneNews.API.Models;
using Voom.DroneNews.API.Repositories.Interfaces;
using Voom.DroneNews.API.Services.Interfaces;

namespace Voom.DroneNews.API.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly List<Article> _articles;
        Dictionary<string, HashSet<Article>> _articlesIndexed;
        private readonly INewsProviderService _newsProvider;
        public NewsRepository(INewsProviderService newsProvider)
        {
            _articles = new List<Article>();
            _articlesIndexed = new Dictionary<string, HashSet<Article>>(StringComparer.OrdinalIgnoreCase);
            _newsProvider = newsProvider;
            UpdateNews(null);
        } 
        public void AddNews(Article article) 
        {
            _articles.Add(article);
            BuildIndex(article);
        }
        public List<Article> SearchNews(string query, int skip, int take) 
        {
            List<Article> result;

            if (string.IsNullOrEmpty(query))
            {
                result =  _articles;   
            }
            else
            {
                var keywords = query.Split(' ').ToList();

                result = _articlesIndexed.ContainsKey(keywords[0])
                   ? new List<Article>(_articlesIndexed[keywords[0]])
                   : new List<Article>();

                for (int i = 1; i < keywords.Count; i++)
                {
                    if (_articlesIndexed.ContainsKey(keywords[i]))
                    {
                        result = result.Intersect(_articlesIndexed[keywords[i]]).ToList();
                    }
                    else
                    {
                        result.Clear();
                        break;
                    }
                }
            }
              
            return result.Skip(skip).Take(take).ToList();
        }
        public void UpdateNews(DateTime? from)
        {
            var articles = _newsProvider.GetDronesNews(from);
            InitiateData(articles);
        }
        private void BuildIndex(Article article) 
        {
            BuildWordIndex(article.Title, article);
            BuildWordIndex(article.Description, article);
            BuildWordIndex(article.Content, article);
        }
        private void BuildWordIndex(string text, Article article)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text.Split(' ').ToList().ForEach(word =>
                {
                    if (!_articlesIndexed.ContainsKey(word))
                    {
                        _articlesIndexed[word] = new HashSet<Article>();
                    }

                    _articlesIndexed[word].Add(article);
                });
            }
        }
        private void InitiateData(List<Article> articles) 
        {
            foreach (var article in articles) 
            {
                 AddNews(article);  
            }
        }
    }
}
