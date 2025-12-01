namespace CinemaSystem.Services.DesignPatterns.Factory.Singleton
{
    public class FactorySingleton
    {
        private static FactorySingleton? instance;
        private readonly Dictionary<TicketType, ITicketFactory> factories;
        private static readonly object lockObject = new object();
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
                    if (instance == null)
                    {
                        instance = new FactorySingleton();
                    }
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
