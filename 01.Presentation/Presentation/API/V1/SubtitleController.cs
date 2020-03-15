using Application.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.VMs;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Presentation.API.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SubtitleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISubtitleService _subtitleService;

        public SubtitleController(
            ISubtitleService subtitleService,
            IMapper mapper
        )
        {
            _subtitleService = subtitleService;
            _mapper = mapper;
        }

        [HttpGet("{ID}")]
        public IActionResult Get(Guid ID)
        {
            var subtitle = _subtitleService.GetByID(ID);

            if (subtitle == null) return NotFound();

            return Ok(_mapper.Map<SubtitleVM>(subtitle));
        }

        [Authorize]
        [HttpGet("fansubs/{fansubAcronym}/animes/{animeSlug}/episodes/{episodeNumber}")]
        public IActionResult Get(string fansubAcronym, string animeSlug, int episodeNumber)
        {
            var identityID = User.Claims.GetIdentityID();

            var subtitle = _subtitleService.GetForEdit(fansubAcronym, animeSlug, episodeNumber, identityID);

            if (subtitle == null) return NotFound();

            return Ok(_mapper.Map<SubtitleVM>(subtitle));
        }

        [Authorize]
        [HttpPost("fansubs/{fansubAcronym}/animes/{animeSlug}/episodes/{episodeNumber}/subtitle")]
        public async Task<IActionResult> CreateSubtitle(string fansubAcronym, string animeSlug, int episodeNumber, [FromForm]SubtitleDTO subtitleDTO)
        {
            var identityID = User.Claims.GetIdentityID();

            var subtitle = await _subtitleService.Create(subtitleDTO, fansubAcronym, animeSlug, episodeNumber, identityID);

            return Ok(subtitle);
        }

        [Authorize]
        [HttpPost("fansubs/{fansubAcronym}/animes/{animeSlug}/episodes/{episodeNumber}/subtitle/partial")]
        public async Task<IActionResult> CreateSubtitlePartial(string fansubAcronym, string animeSlug, int episodeNumber, [FromForm]SubtitlePartialDTO subtitlePartialDTO)
        {
            throw new NotImplementedException();
        }
    }
}
