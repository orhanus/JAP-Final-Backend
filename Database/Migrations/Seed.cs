using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Database.Migrations
{
    public class Seed
    {
        private static async Task<ICollection<Actor>> ExistingActorsInDb(ICollection<Actor> actors, DataContext context)
        {
            List<Actor> existingActorList = new();

            foreach (var actor in actors)
            {
                var existingActor = await context.Actors
                    .FirstOrDefaultAsync(a => a.Firstname == actor.Firstname && a.Lastname == actor.Lastname);

                if (existingActor != null)
                    existingActorList.Add(existingActor);
                else existingActorList.Add(actor);
            }
            return existingActorList;
        }
        public static async Task SeedShows(DataContext context)
        {
            if (await context.Media.AnyAsync()) return;

            var showData = await System.IO.File.ReadAllTextAsync("../Database/Migrations/ShowSeedData.json");
            var shows = JsonSerializer.Deserialize<List<Media>>(showData);

            Random random = new Random();

            foreach (var show in shows)
            {
                show.Actors = await ExistingActorsInDb(show.Actors, context);
                show.Screenings = new List<Screening> { new Screening { ScreeningTime = DateTime.Now.AddDays(random.Next(-50, 50)) } };
                context.Media.Add(show);
            }



            await context.SaveChangesAsync();
        }

    }
}
