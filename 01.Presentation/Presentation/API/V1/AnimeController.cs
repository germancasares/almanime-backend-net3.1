using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Application.Services.Interfaces;
using AutoMapper;
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
            var anime = _animeService.GetByID(ID);

            if (anime == null) return NotFound();
 
            return Ok(_mapper.Map<AnimeVM>(anime));
        }

        [HttpGet("{ID}/chapters")]
        public IActionResult GetChapters(Guid ID)
        {
            var chapters = _animeService.GetChapters(ID);

            return Ok(_mapper.Map<IEnumerable<ChapterVM>>(chapters));
        }

        //TOOD: Make slug get the ID of the anime, and then call Get from above.
        [HttpGet("slug/{slug}")]
        public IActionResult GetBySlug(string slug)
        {
            var anime = _animeService.GetBySlug(slug);

            if (anime == null) return NotFound();

            return Ok(_mapper.Map<AnimeVM>(anime));
        }

        //TODO: Write endpoint to access chapters via slug

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
    }
}