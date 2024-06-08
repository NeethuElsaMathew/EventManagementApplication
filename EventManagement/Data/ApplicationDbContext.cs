using Microsoft.EntityFrameworkCore;
using EventManagement.Model;

namespace EventManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registration { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed a default user for event creation
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                EmailAddress = "default_event_creator@example.com",
                FirstName = "Neethu Elsa",
                LastName = "Mathew",
                Username = "user_event_creator",
                Password = "eE!6&)W*Ppx",
                Role = "EventCreator"
            });
        }
    }
}
