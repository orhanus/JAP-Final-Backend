using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.DTOs
{
    public class MediaDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CoverImageUrl { get; set; }
        public MediaType Type { get; set; }
        public ICollection<ActorDto> Actors { get; set; }
        public ICollection<ScreeningDto> Screenings { get; set; }
        public double AverageRating { get; set; }
    }
}
