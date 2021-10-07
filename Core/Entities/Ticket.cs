using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Tickets")]
    public class Ticket : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ScreeningId { get; set; }
        public Screening Screening { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
    }
}
