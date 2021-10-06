using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IActorRepository
    {
        Task<Actor> GetActorByFullNameAsync(string firstname, string lastname);
        Task<ICollection<Actor>> GetActorsAsync();
        Task<bool> AddActorAsync(Actor actor);

    }
}
