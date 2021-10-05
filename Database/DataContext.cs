using Core.Entities;
using Core.Entities.Procedures;
using Microsoft.EntityFrameworkCore;

namespace Database
{

    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MostRated>().HasNoKey();
            modelBuilder.Entity<MostScreened>().HasNoKey();
            modelBuilder.Entity<MostWatched>().HasNoKey();
        }

        public DbSet<Media> Media { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<MostRated> TopMostRatings { get; set; }
        public DbSet<MostScreened> TopMostScreenings { get; set; }
        public DbSet<MostWatched> TopMostSoldTickets { get; set; }
    }
}
