using System.Globalization;

namespace CinemaSystem.Services
{
    public class MailService
    {
        public void SendReservationConfirmation(
        string recipientEmail,
        string recipientName,
        string reservationCode,
        string eventName,
        DateTime eventDate,
        List<string> seatInfo,
        decimal totalPrice)
        {
            var detailUrl = $"https://localhost:7116/reservation/detail/{reservationCode}";
            Console.WriteLine("\n------------------------------------");
            Console.WriteLine("             Reservation            ");
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"TO: {recipientEmail}");
            Console.WriteLine($"NAME: {recipientName}");
            Console.WriteLine($"RESERVATION CODE: {reservationCode}");
            Console.WriteLine($"EVENT: {eventName}");
            Console.WriteLine($"LINK: {detailUrl}");
            Console.WriteLine($"DATE: {eventDate:dddd, MMMM dd, yyyy 'at' HH:mm}");
            Console.WriteLine($"SEATS:");
            foreach (var seat in seatInfo)
            {
                Console.WriteLine($"   {seat}");
            }
            Console.WriteLine($"TOTAL PRICE: {totalPrice:C}");
            Console.WriteLine("------------------------------------\n");

        }
        public void SendReservationCancellation(
        string recipientEmail,
        string recipientName,
        string reservationCode,
        string eventName)
        {
            Console.WriteLine("\n------------------------------------");
            Console.WriteLine("        Reservation cancelled       ");
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"TO: {recipientEmail}");
            Console.WriteLine($"NAME: {recipientName}");
            Console.WriteLine($"RESERVATION CODE: {reservationCode}");
            Console.WriteLine($"EVENT: {eventName}");
            Console.WriteLine("------------------------------------\n");

        }
    }
}
    
