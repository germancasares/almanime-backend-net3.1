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
        [HttpPost]
        public async Task<IActionResult> CreateSubtitle([FromForm]SubtitleDTO subtitleDTO)
        {
            var identityID = User.GetIdentityID();

            var subtitle = await _subtitleService.Create(subtitleDTO, identityID);

            return Ok(subtitle);
        }
    }
}
