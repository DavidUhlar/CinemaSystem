using CinemaSystem.Models;
using CinemaSystem.Models.Enums;
using CinemaSystem.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CinemaSystem.Data
{
    public class CinemaDbContext(DbContextOptions<CinemaDbContext> options) : DbContext(options)
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<FilmShow> FilmShows { get; set; }
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<CateringItem> CateringItems { get; set; }
        public DbSet<LoginAccount> LoginAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                .HasDiscriminator<EventType>(e => e.Type)
                .HasValue<FilmShow>(EventType.Film)
                .HasValue<Concert>(EventType.Concert);

            modelBuilder.Entity<Seat>()
                .HasOne(s => s.CinemaHall)
                .WithMany(h => h.Seats)
                .HasForeignKey(s => s.CinemaHallId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.CinemaHall)
                .WithMany(ch => ch.Events)
                .HasForeignKey(e => e.CinemaHallId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Event)
                .WithMany(f => f.Tickets)
                .HasForeignKey(t => t.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Seat)
                .WithMany()
                .HasForeignKey(t => t.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Reservation)
                .WithMany(r => r.Tickets)
                .HasForeignKey(t => t.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
            .HasOne(t => t.FoodItem)
            .WithMany()
            .HasForeignKey(t => t.FoodItemId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.DrinkItem)
                .WithMany()
                .HasForeignKey(t => t.DrinkItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasIndex(t => new { t.EventId, t.SeatId })
                .IsUnique();

            modelBuilder.Entity<Ticket>()
                .Property(t => t.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.TotalPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Event>()
                .Property(e => e.BasePrice)
                .HasPrecision(10, 2);

            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginAccount>().HasData(
                new LoginAccount { Id = 1, Username = "admin", PasswordHash = HashPasswordForSeed("admin123"), 
                    Name = "Admin", Surname = "Admin"
                }
            );
            
            var halls = new List<CinemaHall>
            {
                new() { Id = 1, Name = "Hall A", TotalRows = 5, SeatsPerRow = 8 },
                new() { Id = 2, Name = "Hall B", TotalRows = 6, SeatsPerRow = 10 },
                new() { Id = 3, Name = "Hall C", TotalRows = 3, SeatsPerRow = 6 }
            };
            modelBuilder.Entity<CinemaHall>().HasData(halls);

            var seats = GenerateSeatsForHalls(halls);
            modelBuilder.Entity<Seat>().HasData(seats);

            var filmShows = GenerateFilmShows();
            modelBuilder.Entity<FilmShow>().HasData(filmShows);

            var customers = GenerateCustomers();
            modelBuilder.Entity<Customer>().HasData(customers);

            var catering = GenerateCatering();
            modelBuilder.Entity<CateringItem>().HasData(catering);

            var reservations = new List<Reservation>
            {
                new() {
                    Id = 1,
                    ReservationCode = "DUMMY-1",
                    CreatedAt = DateTime.Now,
                    Status = ReservationStatus.Pending,
                    Type = ReservationTypeEnum.Standard,
                    Purpose = ReservationPurpose.None,
                    ReservationNote = "Seeded dummy reservation",
                    TotalPrice = 10.00m,
                    CustomerId = 1
                }
            };
            modelBuilder.Entity<Reservation>().HasData(reservations);

            var tickets = new List<Ticket>
            {
                new() {
                    Id = 1,
                    Price = 10.00m,
                    Type = TicketType.Standard,
                    EventId = 1,
                    ReservationId = 1,
                    SeatId = 1,
                    FoodItemId = null,
                    DrinkItemId = null,
                    TotalPrice = 10.00m,
                    TotalDescription = "Standard ticket (seed)"
                }
            };
            modelBuilder.Entity<Ticket>().HasData(tickets);

        }

        private static List<Seat> GenerateSeatsForHalls(List<CinemaHall> halls)
        {
            var seats = new List<Seat>();
            int seatId = 1;

            foreach (var hall in halls)
            {
                for (int row = 1; row <= hall.TotalRows; row++)
                {
                    for (int num = 1; num <= hall.SeatsPerRow; num++)
                    {
                        seats.Add(new Seat
                        {
                            Id = seatId++,
                            Row = row,
                            Number = num,
                            CinemaHallId = hall.Id
                        });
                    }
                }
            }

            return seats;
        }

        private static string HashPasswordForSeed(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }

        private static List<FilmShow> GenerateFilmShows()
            {
            return [ 
                new() {
                    Id = 1,
                    Title = "Inception",
                    Description = "Thriller by Christopher Nolan.",
                    AgeRestriction = AgeEnum.Age15,
                    Genre = Genre.Thriller,
                    LengthInMinutes = 148,
                    Director = "Christopher Nolan",
                    BasePrice = 10.00m,
                    CinemaHallId = 1,
                    StartTime = DateTime.Now.AddHours(2),
                    Type = EventType.Film
                },
                new() {
                    Id = 2,
                    Title = "The Godfather",
                    Description = "A classic mafia drama.",
                    AgeRestriction = AgeEnum.Age18,
                    Genre = Genre.Drama,
                    LengthInMinutes = 175,
                    Director = "Francis Ford Coppola",
                    BasePrice = 10.50m,
                    CinemaHallId = 2,
                    StartTime = DateTime.Now.AddHours(3),
                    Type = EventType.Film
                },
                new() {
                    Id = 3,
                    Title = "Toy Story",
                    Description = "An animated adventure for all ages.",
                    AgeRestriction = AgeEnum.Age12,
                    Genre = Genre.Animation,
                    LengthInMinutes = 81,
                    Director = "John Lasseter",
                    BasePrice = 9.50m,
                    CinemaHallId = 3,
                    StartTime = DateTime.Now.AddHours(4),
                    Type = EventType.Film
                }
                
            ];
        }

        private static List<Customer> GenerateCustomers()
        {
            return
            [
                new Customer { Id = 1, FirstName = "Jozko", LastName="Ferko", Email = "mail@gmail.com" }
            ];
        }

        private static List<CateringItem> GenerateCatering()
        {
            return
            [
                // popcorn
                new() { Id = 1, Type = CateringType.Popcorn, Size = CateringSize.Small, Price = 2.50m },
                new() { Id = 2, Type = CateringType.Popcorn, Size = CateringSize.Medium, Price = 3.50m },
                new() { Id = 3, Type = CateringType.Popcorn, Size = CateringSize.Large, Price = 4.50m },
                new() { Id = 4, Type = CateringType.Popcorn, Size = CateringSize.XXL, Price = 6.00m },
        
                // drink
                new() { Id = 5, Type = CateringType.Drink, Size = CateringSize.Small, Price = 2.00m },
                new() { Id = 6, Type = CateringType.Drink, Size = CateringSize.Medium, Price = 3.00m },
                new() { Id = 7, Type = CateringType.Drink, Size = CateringSize.Large, Price = 4.00m },
                new() { Id = 8, Type = CateringType.Drink, Size = CateringSize.XXL, Price = 5.50m }
            ];
        }
    }
}