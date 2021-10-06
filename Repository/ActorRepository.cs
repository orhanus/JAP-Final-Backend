using Core.Entities;
using Core.Interfaces.Repositories;
using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ActorRepository : IActorRepository
    {
        private readonly DataContext _context;

        public ActorRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddActorAsync(Actor actor)
        {
            _context.Actors.Add(actor);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Actor> GetActorByFullNameAsync(string firstname, string lastname)
        {
            return await _context.Actors
                .FirstOrDefaultAsync(actor => actor.Firstname == firstname && actor.Lastname == lastname);
        }

        public async Task<ICollection<Actor>> GetActorsAsync()
        {
            return await _context.Actors.ToListAsync();
        }
    }
}
