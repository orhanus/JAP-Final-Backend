using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.DTOs
{
    public class PaginatedResponseDto<T>
    {
        public List<T> Data { get; set; }
        public PaginationDto Pagination { get; set; }
    }
}
