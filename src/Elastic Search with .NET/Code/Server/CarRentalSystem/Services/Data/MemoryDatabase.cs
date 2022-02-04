namespace CarRentalSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using StackExchange.Redis;

    public class MemoryDatabase : IMemoryDatabase
    {
        private readonly IConnectionMultiplexer connection;

        public MemoryDatabase(IConnectionMultiplexer connection) 
            => this.connection = connection;

        public Task<long> Increment(string key)
        {
            var database = this.connection.GetDatabase();

            return database.StringIncrementAsync(key);
        }

        public Task<double> IncrementSortedSet(string sortedSetKey, int value)
        {
            var database = this.connection.GetDatabase();

            return database.SortedSetIncrementAsync(sortedSetKey, value, 1);
        }

        public async Task<IEnumerable<TValue>> RangeSortedSet<TValue>(string sortedSetKey, int start, int end)
        {
            var database = this.connection.GetDatabase();

            var result = await database.SortedSetRangeByRankAsync(sortedSetKey, start, end);

            var data = new List<TValue>(result.Select(r => (TValue)r.Box()));

            return data;
        }

        public Task SetHash(string hashKey, IDictionary<string, object> values)
        {
            throw new System.NotImplementedException();
        }

        public async Task<TValue> Get<TValue>(string key)
        {
            var database = this.connection.GetDatabase();

            var result = await database.StringGetAsync(key);

            return (TValue)result.Box();
        }
    }
}
