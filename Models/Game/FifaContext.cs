using Microsoft.EntityFrameworkCore;

namespace StudentFunctions.Models.Game
{
    public class FifaContext : DbContext
    {
        public FifaContext(DbContextOptions<FifaContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
                optionsBuilder.UseSqlServer(connectionString); // Use the connection string from the environment variable
            }
        }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("games"); // Ensure the table name matches SQL

            modelBuilder.Entity<Game>()
                .Property(g => g.Created)
                .HasDefaultValueSql("GETDATE()"); // Ensure the Created column is auto-populated

            modelBuilder.Entity<Game>()
                .HasCheckConstraint("CK_Gender", "[Gender] IN ('Men', 'Women')"); // Apply check constraint on entity level, not property

            modelBuilder.Entity<Game>()
                .HasCheckConstraint("CK_Continent", "[Continent] IN ('South America', 'Europe', 'North America', 'Asia', 'Africa', 'Oceania')"); // Apply check constraint on entity level, not property
        }

    }
}
