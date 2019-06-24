using Application.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Presentation.API.V1
{
    [ValidateModel]
    [Route("api/v1/[controller]")]
    public class FansubController : ControllerBase
    {

        private readonly ILogger<FansubController> _logger;
        private readonly IMapper _mapper;
        private readonly IFansubService _fansubService;

        public FansubController(
            ILogger<FansubController> logger,
            IFansubService fansubService,
            IMapper mapper
        )
        {
            _logger = logger;
            _fansubService = fansubService;
            _mapper = mapper;
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] FansubDTO fansubDTO)
        {
            var fansub = _fansubService.Create(fansubDTO);

            return Ok(fansub);
        }

        [HttpDelete("{fansubID}")]
        public void Delete(Guid fansubID)
        {
            _fansubService.Delete(fansubID, new Guid("7E542F5F-BF09-453D-8D29-D81D6117FD8F"));
        }
    }
}
