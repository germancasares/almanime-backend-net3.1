using Application.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;

namespace Application
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Episode GetByID(Guid guid) => _unitOfWork.Episodes.GetByID(guid);
        public Episode GetByAnimeIDAndNumber(Guid guid, int number) => _unitOfWork.Episodes.GetByAnimeIDAndNumber(guid, number);
        public Episode GetByAnimeSlugAndNumber(string animeSlug, int number) => _unitOfWork.Episodes.GetByAnimeSlugAndNumber(animeSlug, number);
        public IEnumerable<Episode> GetByAnimeID(Guid guid) => _unitOfWork.Episodes.GetByAnimeID(guid);
        public IEnumerable<Episode> GetByAnimeSlug(string animeSlug) => _unitOfWork.Episodes.GetByAnimeSlug(animeSlug);

        public EpisodeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Episode Create(EpisodeDTO episodeDTO)
        {
            var episode = _unitOfWork.Episodes.Create(_mapper.Map<EpisodeDTO, Episode>(episodeDTO));

            episode.Anime = _unitOfWork.Animes.GetBySlug(episodeDTO.AnimeSlug);

            _unitOfWork.Save();

            return episode;
        }

        public void Update(EpisodeDTO episodeDTO)
        {
            var episode = GetByAnimeSlugAndNumber(episodeDTO.AnimeSlug, episodeDTO.Number);

            if (episode == null) return;

            _unitOfWork.Episodes.Update(_mapper.Map(episodeDTO, episode));
            _unitOfWork.Save();
        }
    }
}
