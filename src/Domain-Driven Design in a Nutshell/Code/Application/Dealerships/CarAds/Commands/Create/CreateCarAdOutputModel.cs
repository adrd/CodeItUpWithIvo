namespace CarRentalSystem.Application.Dealerships.CarAds.Commands.Create
{
    public class CreateCarAdOutputModel
    {
        internal CreateCarAdOutputModel(int carAdId) 
            => this.CarAdId = carAdId;

        public int CarAdId { get; }
    }
}
