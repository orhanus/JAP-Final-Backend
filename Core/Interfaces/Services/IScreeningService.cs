using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IScreeningService
    {
        Task<bool> AddUserToScreeningAsync(int mediaId, string username);
    }
}
