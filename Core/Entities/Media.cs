using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Media")]
    public class Media : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CoverImageUrl { get; set; }
        public MediaType Type  { get; set; }
        public ICollection<Actor> Actors { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Screening> Screenings { get; set; } = new List<Screening>();
    }
}
