using Domain.DTOs;
using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IAnimeService
    {
        Anime GetByKitsuID(int kitsuID);
        Anime GetBySlug(string slug);

        Anime Create(AnimeDTO animeDTO);
        void Update(AnimeDTO animeDTO);
    }
}
