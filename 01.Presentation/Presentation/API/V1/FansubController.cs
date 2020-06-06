using Application.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.VMs.Derived;
using Domain.VMs;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.API.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FansubController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFansubService _fansubService;

        public FansubController(
            IFansubService fansubService,
            IMapper mapper
        )
        {
            _fansubService = fansubService;
            _mapper = mapper;
        }

        [HttpHead("fullname/{fullname}")]
        public IActionResult CheckFullNameAvailability(string fullname)
        {
            if (_fansubService.ExistsFullName(fullname)) return Ok();
            return NotFound();
        }

        [HttpHead("acronym/{acronym}")]
        public IActionResult CheckAcronymAvailability(string acronym)
        {
            if (_fansubService.ExistsAcronym(acronym)) return Ok();
            return NotFound();
        }

        [HttpGet("{acronym}")]
        public IActionResult Get(string acronym)
        {
            var fansub = _fansubService.GetByAcronym(acronym);

            if (fansub == null) return NotFound();

            return Ok(_mapper.Map<FansubVM>(fansub));
        }

        [HttpGet("{acronym}/completedAnimes")]
        public IActionResult GetCompletedAnimes(
            string acronym,
            [FromQuery]int page = 1,
            [FromQuery]int size = 8,
            [FromQuery]bool includeMeta = false
        )
        {
            if (size > 25) return BadRequest("Maximun size is 25");

            var animes = _fansubService
                .GetCompletedAnimes(acronym);

            var animesInPage = animes.OrderByDescending(a => a.FinishedDate)
                .Page(page, size)
                .ToList();

            var animePage = new ModelWithMetaVM<List<FansubAnimeVM>>
            {
                Models = animesInPage
            };

            if (includeMeta)
            {
                animePage.Meta = new PaginationMetaVM
                {
                    BaseUrl = Request.GetPath(),
                    Count = animes.Count(),
                    CurrentPage = page,
                    PageSize = size,
                };
            }

            return Ok(animePage);
        }

        [HttpGet("{acronym}/completedEpisodes")]
        public IActionResult GetCompletedEpisodes(
            string acronym,
            [FromQuery]int page = 1,
            [FromQuery]int size = 8,
            [FromQuery]bool includeMeta = false
        )
        {
            if (size > 25) return BadRequest("Maximun size is 25");

            var episodes = _fansubService
                .GetCompletedEpisodes(acronym);

            var episodesInPage = episodes
                .OrderByDescending(e => e.FinishedDate)
                .Page(page, size)
                .ToList();

            var episodePage = new ModelWithMetaVM<List<FansubEpisodeVM>>
            {
                Models = episodesInPage
            };

            if (includeMeta)
            {
                episodePage.Meta = new PaginationMetaVM
                {
                    BaseUrl = Request.GetPath(),
                    Count = episodes.Count(),
                    CurrentPage = page,
                    PageSize = size,
                };
            }

            return Ok(episodePage);
        }

        [HttpGet("{acronym}/members")]
        public IActionResult GetMembers(string acronym) => Ok(_fansubService.GetMembers(acronym));

        [Authorize]
        [HttpPost]
        public IActionResult Create(FansubDTO fansubDTO)
        {
            var identityID = User.GetIdentityID();

            var fansub = _fansubService.Create(fansubDTO, identityID);

            return Ok(fansub.ID);
        }

        [Authorize]
        [HttpDelete("{fansubID}")]
        public void Delete(Guid fansubID)
        {
            var identityID = User.GetIdentityID();

            _fansubService.Delete(fansubID, identityID);
        }
    }
}
