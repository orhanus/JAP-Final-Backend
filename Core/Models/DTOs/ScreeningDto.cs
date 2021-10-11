using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.DTOs
{
    public class ScreeningDto
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; }
        public DateTime ScreeningTime { get; set; }
        public AddressDto Address { get; set; }

    }
}
