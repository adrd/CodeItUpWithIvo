namespace ExtensionMethods.DomainModels
{
    public class Cat
    {
        public Cat(string name, int age, string owner)
        {
            this.Name = name;
            this.Age = age;
            this.Owner = owner;
        }

        public string Name { get; }

        public int Age { get; }

        public string Owner { get; }
    }
}
