namespace DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CatService
    {
        public IEnumerable<CatResult> RandomCatsFromToday()
        {
            var database = new DbContext();

            var today = DateTime.Now;
            var startOfToday = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);

            var random = new Random();
            var totalCatNames = random.Next(10, 51);

            var allCats = database
                .GetCats()
                .Where(c => c.AddedOn > startOfToday)
                .Take(totalCatNames)
                .Select(c => new CatResult
                {
                    Name = c.Name
                })
                .ToList();

            return allCats;
        }
    }
}
