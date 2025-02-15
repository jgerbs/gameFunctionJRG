using Microsoft.EntityFrameworkCore;
using System;

namespace StudentFunctions.Models.Game
{
    public class FifaContext : DbContext
    {
        public FifaContext(DbContextOptions<FifaContext> options)
            : base(options)
        {
        }

        // OnConfiguring method to set up the context with the connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
            optionsBuilder.UseSqlServer(connectionString); // Use the connection string from the environment variable
        }

        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("games", t =>
            {
                t.HasCheckConstraint("CK_Gender", "[Gender] IN ('Men', 'Women')");
                t.HasCheckConstraint("CK_Continent", "[Continent] IN ('South America', 'Europe', 'North America', 'Asia', 'Africa', 'Oceania')");
            });
        }
    }
}
