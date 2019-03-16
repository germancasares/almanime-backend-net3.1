using Domain.DTOs;
using Domain.Models;
using System.Collections.Generic;

namespace Application.Services.Interfaces
{
    public interface IAnimeService
    {
        Anime GetByKitsuID(int kitsuID);
        Anime GetBySlug(string slug);

        Anime Create(AnimeDTO animeDTO);
        void Update(AnimeDTO animeDTO);
        IEnumerable<Anime> GetSeason(int year, int month);
    }
}
