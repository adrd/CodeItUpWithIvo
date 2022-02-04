namespace DependencyInjection
{
    using System;
    using SimpleInjector;

    public class Program
    {
        public static void Main()
        {
            var container = new Container();

            container.Register<IAppSettings, AppSettings>();
            container.Register<IDbContext, DbContext>();
            container.Register<IRandomProvider, RandomProvider>();
            container.Register<ICurrentTimeProvider, CurrentTimeProvider>();

            container.Verify();

            var catService = container.GetInstance<CatService>();

            var randomCats = catService.RandomCatsFromToday();

            foreach (var randomCat in randomCats)
            {
                Console.WriteLine(randomCat.Name);
            }
        }
    }
}
