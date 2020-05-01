using Application.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.Enums;
using Domain.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AnimeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Anime GetByID(Guid guid) => _unitOfWork.Animes.GetByID(guid);
        public Anime GetByKitsuID(int kitsuID) => _unitOfWork.Animes.GetByKitsuID(kitsuID);
        public Anime GetBySlug(string slug) => _unitOfWork.Animes.GetBySlug(slug);
        public IEnumerable<Episode> GetEpisodes(Guid guid) => _unitOfWork.Animes.GetByID(guid).Episodes;
        public IEnumerable<Episode> GetEpisodesBySlug(string slug) => _unitOfWork.Animes.GetBySlug(slug).Episodes;
        public Episode GetEpisode(Guid animeID, int number) => _unitOfWork.Animes.GetByID(animeID).Episodes.SingleOrDefault(c => c.Number == number);
        public Episode GetEpisodeBySlug(string slug, int number) => _unitOfWork.Animes.GetBySlug(slug).Episodes.SingleOrDefault(c => c.Number == number);

        public int GetAnimesInSeason(int year, ESeason season) => _unitOfWork.Animes.GetAnimesInSeason(year, season);
        public IEnumerable<Anime> GetSeason(int year, ESeason season) => _unitOfWork.Animes.GetSeason(year, season);

        public Anime Create(AnimeDTO animeDTO)
        {
            var anime = _unitOfWork.Animes.Create(_mapper.Map<AnimeDTO, Anime>(animeDTO));

            _unitOfWork.Save();

            return anime;
        }

        public void Update(AnimeDTO animeDTO)
        {
            var anime = GetByKitsuID(animeDTO.KitsuID);

            if (anime == null) return;

            _unitOfWork.Animes.Update(_mapper.Map(animeDTO, anime));
            _unitOfWork.Save();
        }
    }
}
