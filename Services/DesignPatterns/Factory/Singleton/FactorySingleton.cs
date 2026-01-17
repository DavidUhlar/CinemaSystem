using CinemaSystem.Models.Enums;

namespace CinemaSystem.Services.DesignPatterns.Factory.Singleton
{
    public class FactorySingleton
    {
        private static FactorySingleton? instance;
        private readonly Dictionary<TicketType, ITicketFactory> factories;
        private static readonly Lock lockObject = new();
        private FactorySingleton()
        {
            factories = new Dictionary<TicketType, ITicketFactory>
            {
                { TicketType.Standard, new StandardTicketFactory() },
                { TicketType.Student, new StudentTicketFactory() },
                { TicketType.Senior, new SeniorTicketFactory() },
                { TicketType.VIP, new VipTicketFactory() }
            };
        }
        public static FactorySingleton GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    instance ??= new FactorySingleton();
                }
            }
            return instance;
        }
        public ITicketFactory GetFactory(TicketType type)
        {
            return factories[type];
        }
    }
}
