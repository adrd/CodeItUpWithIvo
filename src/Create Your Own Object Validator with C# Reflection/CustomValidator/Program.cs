namespace CustomValidator
{
    using System;

    public class Program
    {
        public static void Main()
        {
            var cat = new Cat
            {
                Name = "John",
                Age = 2,
                Color = "Black"
            };

            var secondCat = new Cat();

            var validator = new ObjectValidator();

            var result = validator.Validate(cat);
            PrintErrors(result);

            result = validator.Validate(secondCat);
            PrintErrors(result);
        }

        private static void PrintErrors(ValidationResult result)
        {
            Console.WriteLine(result.IsValid ? "Valid" : "Invalid");

            foreach (var (key, value) in result.Errors)
            {
                Console.WriteLine(key);

                foreach (var errorMessage in value)
                {
                    Console.WriteLine($"---{errorMessage}");
                }
            }

            Console.WriteLine(new string('-', 20));
        }
    }
}
