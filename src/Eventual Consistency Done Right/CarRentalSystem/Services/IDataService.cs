namespace CarRentalSystem.Services
{
    using System.Threading.Tasks;
    using Data.Models;

    public interface IDataService<in TEntity>
        where TEntity : class
    {
        Task MarkMessageAsPublished(int messageId);

        Task Save(TEntity entity, params Message[] messages);
    }
}
