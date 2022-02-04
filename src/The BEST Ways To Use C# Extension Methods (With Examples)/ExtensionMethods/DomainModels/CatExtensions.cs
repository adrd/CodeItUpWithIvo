namespace ExtensionMethods.DomainModels
{
    public static class CatExtensions
    {
        public static string GetMaturity(this Cat cat)
        {
            if (cat.Age < 2)
            {
                return "Young";
            }
            else if (cat.Age < 8)
            {
                return "Adult";
            }
            else
            {
                return "Old";
            }
        }
    }
}
