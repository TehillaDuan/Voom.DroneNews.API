using Voom.DroneNews.API.Models;

namespace Voom.DroneNews.API.Repositories.Interfaces
{
    public interface INewsRepository
    {
        void AddNews(Article article);
        List<Article> SearchNews(string query, int skip, int take); 

        void UpdateNews(DateTime? from);   

    }
}
