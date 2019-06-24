using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Constants;
using Domain.Models;
using Domain.VMs;
using Domain.Enums;
using System;
using System.Linq;
using System.Collections.Generic;
using Infrastructure.Helpers;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly ILogger<AnimeController> _logger;
        private readonly IMapper _mapper;
        private readonly IAnimeService _animeService;

        public AnimeController(
            ILogger<AnimeController> logger,
            IAnimeService animeService,
            IMapper mapper
        )
        {
            _logger = logger;
            _animeService = animeService;
            _mapper = mapper;
        }

        [HttpGet("{ID}")]
        public IActionResult Get(Guid ID)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Request recived for path /api/anime/{ID}", ID);

            var anime = _animeService.GetByID(ID);

            if (anime == null) return NotFound();
 
            return Ok(_mapper.Map<Anime, AnimeVM>(anime));
        }

        [HttpGet("Slug={slug}")]
        public IActionResult GetBySlug(string slug)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Request recived for path /api/animes/Slug={slug}", slug);

            var anime = _animeService.GetBySlug(slug);

            if (anime == null) return NotFound();

            return Ok(_mapper.Map<Anime, AnimeVM>(anime));
        }

        [HttpGet("Year={year}&Season={season}")]
        public IActionResult GetSeason(
            int year,
            string season)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Request recived for path /api/animes?Year={year}&Season={season}", year, season);

            var seasonEnum = EnumHelper.GetEnumFromString<ESeason>(season);

            if (!seasonEnum.HasValue) return BadRequest("Season not valid.");

            var requestedSeason = _animeService
                .GetSeason(year, seasonEnum.Value)
                .OrderBy(a => string.IsNullOrWhiteSpace(a.CoverImageUrl))
                .ThenBy(a => a.Name)
                .ToList();

            return Ok(_mapper.Map<List<Anime>, List<AnimeVM>>(requestedSeason));
        }
    }
}