namespace MyTestRunner
{
    using Tests;

    public class Program
    {
        public static void Main()
            => TestRunner.ExecuteTests(typeof(Car));
    }
}
