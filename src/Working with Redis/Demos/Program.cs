namespace Demos
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using StackExchange.Redis;

    public class Program
    {
        private static IConnectionMultiplexer redis;

        public static async Task Main()
        {
            // This value should be stored as a singleton.

            redis = ConnectionMultiplexer
                .Connect("redis-12752.c135.eu-central-1-1.ec2.cloud.redislabs.com:12752,password=Qd6mdtrVm5hK4kxLR7I3Wbkn8NVknjfy");

            await BasicDatabase();
            await PublishSubscribe();
            await FireAndForget();
            await NullValues();
        }

        private static async Task BasicDatabase()
        {
            var database = redis.GetDatabase();

            const string key = "my:value";

            await database.StringSetAsync(key, 10);
            await database.StringIncrementAsync(key);

            Console.WriteLine(await database.StringGetAsync(key));

            const string sortedSetKey = "my:sorted:set";

            await Task.WhenAll(
                database.SortedSetAddAsync(sortedSetKey, "john", 50),
                database.SortedSetAddAsync(sortedSetKey, "george", 40),
                database.SortedSetAddAsync(sortedSetKey, "ana", 60),
                database.SortedSetAddAsync(sortedSetKey, "maria", 80));

            var result = await database.SortedSetRangeByRankAsync(sortedSetKey, 0, 2);

            Console.WriteLine(string.Join(", ", result));
        }

        private static async Task PublishSubscribe()
        {
            var queue = redis.GetSubscriber();

            const string key = "message";

            // Messages are executed in order.
            var orderSubscription = await queue.SubscribeAsync(key);

            orderSubscription.OnMessage(channelMessage =>
            {
                Console.WriteLine(channelMessage.Message);
            });

            // Messages are executed concurrently.
            await queue.SubscribeAsync(key, (channel, message) =>
            {
                Console.WriteLine(message);
            });

            await queue.PublishAsync(key, "Event!");

            Thread.Sleep(1000);
        }

        private static async Task FireAndForget()
        {
            var database = redis.GetDatabase();

            const string key = "my:key";

            await database.StringSetAsync(key, "Forgotten");

            database.KeyExpire(key, TimeSpan.FromMinutes(5), CommandFlags.FireAndForget);

            var value = await database.StringGetAsync(key);

            Console.WriteLine(value);
        }

        private static async Task NullValues()
        {
            var database = redis.GetDatabase();

            const string key = "missing:key";

            var value = await database.StringGetAsync(key);

            Console.WriteLine(value.IsNull);

            var valueAsInt = (int)await database.StringGetAsync(key);

            Console.WriteLine(valueAsInt == 0);

            var valueAsNullableInt = (int?)await database.StringGetAsync(key);

            Console.WriteLine(valueAsNullableInt == null);
        }
    }
}
