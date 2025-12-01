namespace CinemaSystem.Services.DesignPatterns.Builder
{
    public static class DirectorCreator
    {
        public static ReservationDirector CreateStandardDirector()
        {
            return new ReservationDirector(new ReservationBuilder());
        }

        public static ReservationDirector CreateGroupDirector()
        {
            return new ReservationDirector(new GroupReservationBuilder());
        }
    }
}
