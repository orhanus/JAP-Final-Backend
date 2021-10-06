using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Screenings")]
    public class Screening
    {
        public int Id { get; set; }
        public int NumberOfTickets { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int MediaId { get; set; }
        public Media Movie { get; set; }
        public DateTime ScreeningTime { get; set; }
        public ICollection<User> Spectators { get; set; } = new List<User>();
    }
}
