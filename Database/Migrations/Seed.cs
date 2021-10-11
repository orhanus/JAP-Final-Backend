using Core.Entities;
using Microsoft.AspNetCore.Identity;
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
        public static async Task SeedShows(DataContext context, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            if (await context.Media.AnyAsync()) return;

            var showData = await System.IO.File.ReadAllTextAsync("../Database/Migrations/ShowSeedData.json");
            var shows = JsonSerializer.Deserialize<List<Media>>(showData);

            shows[0].Screenings = new List<Screening>
            {
                new Screening { ScreeningTime = DateTime.Now.AddDays(-10) },
                new Screening { ScreeningTime = DateTime.Now.AddDays(50) }
            };

            foreach (var show in shows)
            {
                show.Actors = await ExistingActorsInDb(show.Actors, context);
                //show.Screenings = new List<Screening> 
                //{ 
                //    new Screening { ScreeningTime = DateTime.Now.AddDays(-10) } 
                //};
                context.Media.Add(show);
            }

            var roles = new List<Role>
            {
                new Role{ Name = "Customer" },
                new Role{ Name = "Admin" }
            };

            foreach (var role in roles)
                await roleManager.CreateAsync(role);

            var admin = new User
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Administrator1");

            await userManager.AddToRoleAsync(admin, "Admin");

            await context.SaveChangesAsync();
        }

    }
}
