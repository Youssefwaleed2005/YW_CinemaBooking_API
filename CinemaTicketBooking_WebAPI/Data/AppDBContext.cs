using CinemaTicketBooking_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBooking_WebAPI.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<ShowTime> ShowTimes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public AppDBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Name).IsRequired().HasMaxLength(200);
                entity.Property(m => m.Genre).IsRequired().HasMaxLength(100);
                entity.HasIndex(m => m.Name).IsUnique();
            });

            modelBuilder.Entity<Auditorium>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.RoomNumber).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(150);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(256);
            });

            modelBuilder.Entity<ShowTime>(entity =>
            {
                entity.HasKey(st => st.Id);

                entity.HasOne(st => st.Movie).WithMany(m => m.Shows).HasForeignKey(st => st.MovieId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(st => st.Auditorium).WithMany(a => a.Shows).HasForeignKey(st => st.AuditoriumId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.HasOne(b => b.Customer).WithMany(c => c.Bookings).HasForeignKey(b => b.CustomerId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(b => b.ShowTime).WithMany(st => st.Bookings).HasForeignKey(b => b.ShowTimeId).OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}