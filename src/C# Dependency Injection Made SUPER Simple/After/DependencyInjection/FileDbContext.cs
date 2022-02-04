namespace DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    public class FileDbContext : IDbContext
    {
        public List<Cat> GetCats() 
            => File
                .ReadAllText("cats.txt")
                .Split(Environment.NewLine)
                .Select(catText => catText.Split("-"))
                .Select(catParts => new Cat
                {
                    Id = int.Parse(catParts[0]), 
                    Name = catParts[1], 
                    AddedOn = DateTime.ParseExact(catParts[2], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                })
                .ToList();
    }
}
