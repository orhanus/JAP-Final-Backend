using System.ComponentModel.DataAnnotations;

namespace Core.Models.DTOs
{
    public class AddScreeningDto
    {
        [Required] public int NumberOfTicketsTotal { get; set; }
        [Required] public string Street { get; set; }
        [Required] public string City { get; set; }
        [Required] public string PostalCode { get; set; }
        [Required] public int MediaId { get; set; }
    }
}
