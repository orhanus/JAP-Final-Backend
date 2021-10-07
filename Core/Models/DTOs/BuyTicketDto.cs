using System.ComponentModel.DataAnnotations;

namespace Core.Models.DTOs
{
    public class BuyTicketDto
    {
        [Required] public int ScreeningId { get; set; }
        [Required] public int UserId { get; set; }
        [Required][Range(1, 10)] public int NumberOfTickets { get; set; }
    }
}
