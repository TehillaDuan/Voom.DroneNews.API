using Voom.DroneNews.API.Models.Dto;

namespace Voom.DroneNews.API.Services.Interfaces
{
    public interface INewsService
    {
        List<ArticleDto> SearchNews(string query, int skip, int take);

        void UpdateNews(DateTime? from);
    }
}
