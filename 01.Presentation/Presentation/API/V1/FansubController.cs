using Application.Interfaces;
using AutoMapper;
using Domain.DTOs;
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

        [HttpGet("{acronym}")]
        public IActionResult Get(string acronym)
        {
            var fansub = _fansubService.GetByAcronym(acronym);

            if (fansub == null) return NotFound();

            return Ok(_mapper.Map<FansubVM>(fansub));
        }

        [HttpGet("{acronym}/animes")]
        public IActionResult GetAnimes(string acronym)
        {
            var animes = _fansubService.GetAnimes(acronym);

            return Ok(_mapper.Map<List<AnimeWithEpisodesAndSubtitleVM>>(animes.ToList()));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(FansubDTO fansubDTO)
        {
            var identityID = User.Claims.GetIdentityID();

            var fansub = _fansubService.Create(fansubDTO, identityID);

            return Ok(fansub.ID);
        }

        [Authorize]
        [HttpDelete("{fansubID}")]
        public void Delete(Guid fansubID)
        {
            var identityID = User.Claims.GetIdentityID();

            _fansubService.Delete(fansubID, identityID);
        }
    }
}
