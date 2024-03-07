using Voom.DroneNews.API.Models;

namespace Voom.DroneNews.API.Services.Interfaces
{
    public interface INewsProviderService
    {
        List<Article> GetDronesNews();
    }
}
