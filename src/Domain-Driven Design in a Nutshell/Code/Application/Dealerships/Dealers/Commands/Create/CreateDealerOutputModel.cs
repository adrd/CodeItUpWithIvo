namespace CarRentalSystem.Application.Dealerships.Dealers.Commands.Create
{
    public class CreateDealerOutputModel
    {
        internal CreateDealerOutputModel(int dealerId)
            => this.DealerId = dealerId;

        public int DealerId { get; }
    }
}
