using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.DTOs
{
    public class UpdateMediaDto
    {
        [Required] public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
        [Required] public DateTime ReleaseDate { get; set; }
        [Required] public string CoverImageUrl { get; set; }
        [Required] public MediaType Type { get; set; }
        [Required] public ICollection<ActorDto> Actors { get; set; }
    }
}
