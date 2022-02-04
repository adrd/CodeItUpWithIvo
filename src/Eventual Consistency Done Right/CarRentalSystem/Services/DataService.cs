namespace CarRentalSystem.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;

    public abstract class DataService<TEntity> : IDataService<TEntity>
        where TEntity : class
    {
        protected DataService(DbContext db) => this.Data = db;

        protected DbContext Data { get; }

        protected IQueryable<TEntity> All() => this.Data.Set<TEntity>();

        public async Task MarkMessageAsPublished(int messageId)
        {
            var message = await this.Data
                .Set<Message>()
                .FindAsync(messageId);

            message.MarkAsPublished();

            await this.Data.SaveChangesAsync();
        }

        public async Task Save(
            TEntity entity,
            params Message[] messages)
        {
            this.Data.Update(entity);

            foreach (var message in messages)
            {
                this.Data.Set<Message>().Add(message);
            }

            await this.Data.SaveChangesAsync();
        }
    }
}
