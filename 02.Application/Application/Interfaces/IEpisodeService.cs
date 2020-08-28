using Domain.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IEpisodeService
    {
        Episode Create(EpisodeDTO episodeDTO);
        IEnumerable<Episode> GetByAnimeID(Guid guid);
        Episode GetByAnimeIDAndNumber(Guid guid, int number);
        IEnumerable<Episode> GetByAnimeSlug(string animeSlug);
        Episode GetByAnimeSlugAndNumber(string animeSlug, int number);
        void Update(EpisodeDTO episodeDTO);
    }
}
