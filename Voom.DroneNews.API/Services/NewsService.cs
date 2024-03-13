using Voom.DroneNews.API.Models.Dto;
using Voom.DroneNews.API.Repositories.Interfaces;
using Voom.DroneNews.API.Services.Interfaces;

namespace Voom.DroneNews.API.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public List<ArticleDto> SearchNews(string query, int skip, int take)
        {
            var news = _newsRepository.SearchNews(query, skip, take);

            return news.Select(article => new ArticleDto()
            {
                Author = article.Author,
                Content = article.Content,
                Source = article.Source,
                Description = article.Description,
                ID = article.ID,
                PublishedAt = article.PublishedAt,
                Title = article.Title,
                Url = article.Url   
            }).ToList();
        }

        public void UpdateNews(DateTime? from)
        {
            _newsRepository.UpdateNews(from);   
        }
    }
}
