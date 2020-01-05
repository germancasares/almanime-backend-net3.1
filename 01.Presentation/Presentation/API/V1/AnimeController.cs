using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.VMs;
using Domain.Enums;
using System;
using System.Linq;
using System.Collections.Generic;
using Infrastructure.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly ILogger<AnimeController> _logger;
        private readonly IMapper _mapper;
        private readonly IAnimeService _animeService;
        private readonly IBookmarkService _bookmarkService;

        public AnimeController(
            ILogger<AnimeController> logger,
            IAnimeService animeService,
            IBookmarkService bookmarkService,
            IMapper mapper
        )
        {
            _logger = logger;
            _animeService = animeService;
            _bookmarkService = bookmarkService;
            _mapper = mapper;
        }

        [HttpGet("{ID}")]
        public IActionResult Get(Guid ID)
        {
            var anime = _animeService.GetByID(ID);

            if (anime == null) return NotFound();
 
            return Ok(_mapper.Map<AnimeWithEpisodesVM>(anime));
        }

        [HttpGet("{ID}/episodes")]
        public IActionResult GetEpisodes(Guid ID)
        {
            var episodes = _animeService.GetEpisodes(ID);

            return Ok(_mapper.Map<IEnumerable<EpisodeVM>>(episodes));
        }

        [HttpGet("{ID}/episodes/{number}")]
        public IActionResult GetEpisode(Guid ID, int number)
        {
            var episode = _animeService.GetEpisode(ID, number);

            if (episode == null) return NotFound();

            return Ok(_mapper.Map<EpisodeVM>(episode));
        }

        [HttpGet("slug/{slug}")]
        public IActionResult GetBySlug(string slug)
        {
            var anime = _animeService.GetBySlug(slug);

            if (anime == null) return NotFound();

            return Ok(_mapper.Map<AnimeWithEpisodesVM>(anime));
        }

        [HttpGet("slug/{slug}/episodes")]
        public IActionResult GetEpisodesBySlug(string slug)
        {
            var episodes = _animeService.GetEpisodesBySlug(slug);

            return Ok(_mapper.Map<IEnumerable<EpisodeVM>>(episodes));
        }

        [HttpGet("slug/{slug}/episodes/{number}")]
        public IActionResult GetEpisodeBySlug(string slug, int number)
        {
            var episode = _animeService.GetEpisodeBySlug(slug, number);

            if (episode == null) return NotFound();

            return Ok(_mapper.Map<EpisodeVM>(episode));
        }

        //TODO: Add pagination
        [HttpGet("year/{year}/season/{season}")]
        public IActionResult GetSeason(
            int year,
            string season)
        {
            var seasonEnum = EnumHelper.GetEnumFromString<ESeason>(season);

            if (!seasonEnum.HasValue) return BadRequest("Season not valid.");

            var requestedSeason = _animeService
                .GetSeason(year, seasonEnum.Value)
                .OrderBy(a => string.IsNullOrWhiteSpace(a.CoverImageUrl))
                .ThenBy(a => a.Name)
                .ToList();

            return Ok(_mapper.Map<List<AnimeVM>>(requestedSeason));
        }

        [Authorize]
        [HttpPost("slug/{slug}/bookmark")]
        public IActionResult CreateBoookmark(string slug)
        {


            var identityID = User.Claims.GetIdentityID();

            var bookmark = _bookmarkService.Create(slug, identityID);

            return Ok(_mapper.Map<BookmarkVM>(bookmark));
        }

        [Authorize]
        [HttpDelete("slug/{slug}/bookmark")]
        public void DeleteBoookmark(string slug)
        {
            var identityID = User.Claims.GetIdentityID();

            _bookmarkService.Delete(slug, identityID);
        }
    }
}