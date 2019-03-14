using Domain.Models;

namespace Persistence.Data.Repositories.Interfaces
{
    public interface IAnimeRepository : IBaseRepository<Anime>
    {
        Anime GetByKitsuID(int kitsuID);
        Anime GetBySlug(string slug);
    }
}
