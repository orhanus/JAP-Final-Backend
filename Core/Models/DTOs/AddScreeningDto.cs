using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.DTOs
{
    public class AddScreeningDto
    {
        [Required] public DateTime ScreeningTime { get; set; }
        [Required] [Range(1, Int32.MaxValue)] public int NumberOfTicketsTotal { get; set; }
        [Required] public string Street { get; set; }
        [Required] public string City { get; set; }
        [Required] public string PostalCode { get; set; }
        [Required] public int MediaId { get; set; }
    }
}
