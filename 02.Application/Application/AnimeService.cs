using Application.Services.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.Enums;
using Domain.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;

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
