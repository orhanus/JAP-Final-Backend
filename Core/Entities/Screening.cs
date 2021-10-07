using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Screenings")]
    public class Screening : BaseEntity
    {
        public int Id { get; set; }
        public int NumberOfTicketsTotal { get; set; }
        public double TicketPrice { get; set; }
        public Address Address { get; set; }
        public int MediaId { get; set; }
        public Media Movie { get; set; }
        public DateTime ScreeningTime { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
