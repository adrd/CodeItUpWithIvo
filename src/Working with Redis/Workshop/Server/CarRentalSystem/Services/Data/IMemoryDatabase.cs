namespace CarRentalSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMemoryDatabase
    {
        Task<long> Increment(string key);

        Task<TValue> Get<TValue>(string key);

        Task<double> IncrementSortedSet(string sortedSetKey, int value);

        Task<IEnumerable<TValue>> RangeSortedSet<TValue>(string sortedSetKey, int start, int end);

        Task SetHash(string hashKey, IDictionary<string, object> values);
    }
}
