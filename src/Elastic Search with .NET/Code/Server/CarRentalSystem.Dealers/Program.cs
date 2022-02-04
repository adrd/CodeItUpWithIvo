namespace CarRentalSystem.Dealers
{
    using Infrastructure;

    public class Program
    {
        public static void Main(string[] args)
            => CarRentalHost.Start<Startup>(args);
    }
}
