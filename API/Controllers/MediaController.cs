using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Enums;
using Core.Interfaces.Services;
using Core.Models.DTOs;
using Core.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MediaController : BaseApiController
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpGet("{mediaType}", Name = "GetShows")]
        public async Task<ActionResult<ICollection<MediaDto>>> GetShows(
            [FromQuery]MediaParams mediaParams, 
            MediaType mediaType) 
        {
            var media = await _mediaService.GetMediaAsync(mediaParams, mediaType);
            return Ok(media);
        }
    }
}