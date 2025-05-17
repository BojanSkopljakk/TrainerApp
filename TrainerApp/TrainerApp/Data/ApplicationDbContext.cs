using Microsoft.EntityFrameworkCore;
using TrainerApp.Models;

namespace TrainerApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trainer>().HasData(
                new Trainer { Id = 1, Name = "Ana Petrović", AccessCode = "ana123" },
                new Trainer { Id = 2, Name = "Marko Jovanović", AccessCode = "marko123" },
                new Trainer { Id = 3, Name = "Ivana Milinković", AccessCode = "ivana123" }
            );
        }

    }


}
