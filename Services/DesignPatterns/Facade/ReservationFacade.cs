using CinemaSystem.Data;
using CinemaSystem.Models;
using CinemaSystem.Services.DesignPatterns.Builder;
using CinemaSystem.Services.DesignPatterns.Command;
using CinemaSystem.Services.DesignPatterns.Decorator;
using CinemaSystem.Services.DesignPatterns.Factory;
using CinemaSystem.Services.DesignPatterns.Factory.Singleton;
using CinemaSystem.Services.DesignPatterns.Strategy;

namespace CinemaSystem.Services.DesignPatterns.Facade
{
    public class ReservationFacade
    {
        private readonly CinemaDbContext cinemaDb;
        private readonly FactorySingleton factorySingleton;
        private readonly ReservationInvoker reservationInvoker;
        public ReservationFacade(CinemaDbContext cinemaDbContext, ReservationInvoker reservationInvoker) 
        {
            cinemaDb = cinemaDbContext;
            factorySingleton = FactorySingleton.GetInstance();
            this.reservationInvoker = reservationInvoker;
        }

        public Ticket CreateTicket(int eventId, int seatId, TicketType ticketType)
        {
            Event eventEntity = cinemaDb.Events.Find(eventId)!;
            Seat seatEntity = cinemaDb.Seats.Find(seatId)!;

            ITicketFactory factory = factorySingleton.GetFactory(ticketType);

            Ticket ticket = factory.CreateTicket(eventEntity, seatEntity);

            ApplyPricingStrategy pricingStrategy = new ApplyPricingStrategy(ticketType);
            ticket.Price = pricingStrategy.CalculateFinalPrice(eventEntity);

            return ticket;
        }

        public TicketDto CreateTicketDto(int eventId, int seatId, TicketType ticketType)
        {
            Event eventEntity = cinemaDb.Events.Find(eventId)!;
            Seat seatEntity = cinemaDb.Seats.Find(seatId)!;

            ITicketFactory factory = factorySingleton.GetFactory(ticketType);

            TicketDto ticket = factory.CreateTicketDto(eventEntity, seatEntity);

            ApplyPricingStrategy pricingStrategy = new ApplyPricingStrategy(ticketType);
            ticket.Price = pricingStrategy.CalculateFinalPrice(eventEntity);

            return ticket;
        }

        public void ApplyCateringToTicket(TicketDto ticket, int? foodId, int? drinkId)
        {
            IClientTicket componentDecorator = new ClientTicket(ticket);

            if (foodId.HasValue)
            {
                CateringItem food = cinemaDb.CateringItems.Find(foodId.Value)!;
                componentDecorator = new FoodDecorator(componentDecorator, food);
                ticket.FoodItemId = foodId;
            }

            if (drinkId.HasValue)
            {
                CateringItem drink = cinemaDb.CateringItems.Find(drinkId.Value)!;
                componentDecorator = new DrinkDecorator(componentDecorator, drink);
                ticket.DrinkItemId = drinkId;
            }

            ticket.TotalPrice = componentDecorator.GetTotalPrice();
            ticket.TotalDescription = componentDecorator.GetDescription();
        }

        public Reservation CreateReservation(int customerId, List<Ticket> tickets, ReservationPurpose? reservationPurpose, string? note = null)
        {
            Customer customer = cinemaDb.Customers.Find(customerId)!;

            ReservationDirector director = new ReservationDirector();

            Reservation reservation;
            if (tickets.Count == 1)
            {
                reservation = director.CreateStandardReservationOneTicket(customer, tickets[0], note);
            }
            else if (tickets.Count > 5)
            {
                var reservationPurposeTemp = reservationPurpose ?? ReservationPurpose.None;
                reservation = director.CreateGroupReservation(customer, tickets, reservationPurposeTemp, note);
            }
            else
            {
                reservation = director.CreateStandardReservation(customer, tickets, note);
            }

            CreateReservationCommand command = new CreateReservationCommand(cinemaDb, reservation);
            reservationInvoker.ExecuteCommand(command);
            return reservation;
            
        }

        public void UndoReservation()
        {
            reservationInvoker.UndoCommand();
        }

        public void CancelReservation(int reservationId)
        {
            CancelReservationCommand command = new CancelReservationCommand(cinemaDb, reservationId);
            reservationInvoker.ExecuteCommand(command);
        }
    }
}
