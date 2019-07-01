using Application.Interfaces;
using AutoMapper;
using Domain.VMs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Presentation.API.V1
{
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
    }
}
