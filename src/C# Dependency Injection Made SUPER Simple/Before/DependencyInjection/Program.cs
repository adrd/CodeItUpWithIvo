namespace DependencyInjection
{
    using System;

    public class Program
    {
        public static void Main()
        {
            var catService = new CatService();

            var randomCats = catService.RandomCatsFromToday();

            foreach (var randomCat in randomCats)
            {
                Console.WriteLine(randomCat.Name);
            }
        }
    }
}
