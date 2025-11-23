using Microsoft.EntityFrameworkCore;
using CinemaSystem.Models;

namespace CinemaSystem.Data
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<FilmShow> FilmShows { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

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
                .HasForeignKey(s => s.CinemaHallId);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.CinemaHall)
                .WithMany(ch => ch.Events)
                .HasForeignKey(e => e.CinemaHallId);

            modelBuilder.Entity<FilmShow>()
                .HasOne(f => f.Movie)
                .WithMany(m => m.FilmShows)
                .HasForeignKey(f => f.MovieId);


            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Event)
                .WithMany(f => f.Tickets)
                .HasForeignKey(t => t.EventId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Seat)
                .WithMany()
                .HasForeignKey(t => t.SeatId); ;

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Reservation)
                .WithMany(r => r.Tickets)
                .HasForeignKey(t => t.ReservationId);

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

        private void SeedData(ModelBuilder modelBuilder)
        {

            var halls = new List<CinemaHall>
            {
                new CinemaHall { Id = 1, Name = "Sála 1", TotalRows = 5, SeatsPerRow = 8 },
                new CinemaHall { Id = 2, Name = "Sála 2", TotalRows = 6, SeatsPerRow = 10 },
                new CinemaHall { Id = 3, Name = "VIP Sála", TotalRows = 3, SeatsPerRow = 6 }
            };
            modelBuilder.Entity<CinemaHall>().HasData(halls);

            var seats = GenerateSeatsForHalls(halls);
            modelBuilder.Entity<Seat>().HasData(seats);

            var movies = GenerateMovies();
            modelBuilder.Entity<Movie>().HasData(movies);

            var filmShows = GenerateFilmShows();
            modelBuilder.Entity<FilmShow>().HasData(filmShows);

            var customers = GenerateCustomers();
            modelBuilder.Entity<Customer>().HasData(customers);
        }

        private List<Seat> GenerateSeatsForHalls(List<CinemaHall> halls)
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

        private List<Movie> GenerateMovies()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Title = "Inception", Description = "Thriller by Christopher Nolan.", ageRestriction = AgeEnum.Age15, genre = Genre.SciFi },
                new Movie { Id = 2, Title = "The Godfather", Description = "A classic mafia drama.", ageRestriction = AgeEnum.Age18, genre = Genre.Drama },
                new Movie { Id = 3, Title = "Toy Story", Description = "An animated adventure for all ages.", ageRestriction = AgeEnum.Age12, genre = Genre.Animation }
            };
        }

        private List<FilmShow> GenerateFilmShows()
        {
            return new List<FilmShow>
            {
                new FilmShow { 
                    Id = 1, 
                    MovieId = 1, 
                    CinemaHallId = 1, 
                    StartTime = DateTime.Now.AddHours(2),
                    Type = EventType.Film
                },
                new FilmShow { 
                    Id = 2, 
                    MovieId = 2, 
                    CinemaHallId = 2, 
                    StartTime = DateTime.Now.AddHours(3),
                    Type = EventType.Film
                },
                new FilmShow { 
                    Id = 3, 
                    MovieId = 3, 
                    CinemaHallId = 3, 
                    StartTime = DateTime.Now.AddHours(4),
                    Type = EventType.Film
                }
            };
        }

        private List<Customer> GenerateCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, FirstName = "Jozko", LastName="Ferko", Email = "mail@gmail.com" }
            };
        }
    }
}