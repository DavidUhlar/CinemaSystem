using CinemaSystem.Data;
using CinemaSystem.Models;
using CinemaSystem.Models.Enums;
using CinemaSystem.Services.DesignPatterns.Builder;
using CinemaSystem.Services.DesignPatterns.Command;
using CinemaSystem.Services.DesignPatterns.Decorator;
using CinemaSystem.Services.DesignPatterns.Factory;
using CinemaSystem.Services.DesignPatterns.Factory.Singleton;
using CinemaSystem.Services.DesignPatterns.Strategy;

namespace CinemaSystem.Services.DesignPatterns.Facade
{
    public class ReservationFacade(CinemaDbContext cinemaDbContext, ReservationInvoker reservationInvoker, MailService mailService)
    {
        private readonly CinemaDbContext cinemaDb = cinemaDbContext;
        private readonly FactorySingleton factorySingleton = FactorySingleton.GetInstance();
        private readonly ReservationInvoker reservationInvoker = reservationInvoker;
        private readonly MailService mailService = mailService;

        public Ticket CreateTicket(int eventId, int seatId, TicketType ticketType)
        {
            Event eventEntity = cinemaDb.Events.Find(eventId)!;
            Seat seatEntity = cinemaDb.Seats.Find(seatId)!;

            ITicketFactory factory = factorySingleton.GetFactory(ticketType);

            Ticket ticket = factory.CreateTicket(eventEntity, seatEntity);

            ApplyPricingStrategy pricingStrategy = new(ticketType);
            ticket.Price = pricingStrategy.CalculateFinalPrice(eventEntity);

            return ticket;
        }

        public TicketDto CreateTicketDto(int eventId, int seatId, TicketType ticketType)
        {
            Event eventEntity = cinemaDb.Events.Find(eventId)!;
            Seat seatEntity = cinemaDb.Seats.Find(seatId)!;

            ITicketFactory factory = factorySingleton.GetFactory(ticketType);

            TicketDto ticket = factory.CreateTicketDto(eventEntity, seatEntity);

            ApplyPricingStrategy pricingStrategy = new(ticketType);
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


            Reservation reservation;
            if (tickets.Count > 5)
            {
                ReservationDirector director = DirectorCreator.CreateGroupDirector();
                var reservationPurposeTemp = reservationPurpose ?? ReservationPurpose.None;
                reservation = director.CreateGroupReservation(customer, tickets, reservationPurposeTemp, note);
            }
            else
            {
                ReservationDirector director = DirectorCreator.CreateStandardDirector();
                reservation = director.CreateStandardReservation(customer, tickets, note);
            }

            CreateReservationCommand command = new(cinemaDb, reservation, mailService);
            reservationInvoker.ExecuteCommand(command);
            return reservation;
            
        }

        public void UndoReservation()
        {
            reservationInvoker.UndoCommand();
        }

        public void CancelReservation(int reservationId)
        {
            CancelReservationCommand command = new(cinemaDb, reservationId);
            reservationInvoker.ExecuteCommand(command);
        }
    }
}
