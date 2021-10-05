using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.DTOs
{
    public class RatingDto
    {
        [Required]
        [Range(1, 10, ErrorMessage = "Value for {0} mest be between {1} and {2}")]
        public int Score { get; set; }
    }
}
