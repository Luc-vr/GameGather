using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GameGatherDbContext : DbContext
    {
        public GameGatherDbContext(DbContextOptions<GameGatherDbContext> options) : base(options)
        {
        }

        public GameGatherDbContext()
        {
        }

        // DB sets for each entity
        public DbSet<BoardGame> BoardGames { get; set; }

        public DbSet<BoardGameNight> BoardGameNights { get; set; }

        public DbSet<FoodAndDrinksPreference> FoodAndDrinksPreferences { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardGameNight>()
                .HasOne(bgn => bgn.Host)
                .WithMany(user => user.HostingBoardGameNights)
                .HasForeignKey(bgn => bgn.HostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>().
                HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BoardGameNight>()
               .HasMany(bgn => bgn.Attendees)
               .WithMany(user => user.AttendingBoardGameNights)
               .UsingEntity<Dictionary<string, object>>(
                   "Attendance",
                   j => j
                       .HasOne<User>()
                       .WithMany()
                       .HasForeignKey("UserId")
                       .OnDelete(DeleteBehavior.Restrict),
                   j => j
                       .HasOne<BoardGameNight>()
                       .WithMany()
                       .HasForeignKey("BoardGameNightId")
                       .OnDelete(DeleteBehavior.Cascade),
                   j =>
                   {
                       j.HasKey("UserId", "BoardGameNightId");
                       j.HasIndex("BoardGameNightId");
                   }
               );

            modelBuilder.Entity<BoardGameNight>()
               .HasMany(bgn => bgn.BoardGames)
               .WithMany(bg => bg.BoardGameNights)
               .UsingEntity<Dictionary<string, object>>(
                   "PlayedBoardGames",
                   j => j
                       .HasOne<BoardGame>()
                       .WithMany()
                       .HasForeignKey("BoardGameId")
                       .OnDelete(DeleteBehavior.Restrict),
                   j => j
                       .HasOne<BoardGameNight>()
                       .WithMany()
                       .HasForeignKey("BoardGameNightId")
                       .OnDelete(DeleteBehavior.Cascade),
                   j =>
                   {
                       j.HasKey("BoardGameId", "BoardGameNightId");
                       j.HasIndex("BoardGameNightId");
                   }
               );


        }

    }
}
