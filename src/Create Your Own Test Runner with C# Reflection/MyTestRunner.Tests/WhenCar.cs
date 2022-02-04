namespace MyTestRunner.Tests
{
    using Framework;
    using Shouldly;

    [Subject("Car")]
    public class WhenCarIsStarted
    {
        const string Model = "BMW";

        static Car car;

        Given context = () =>
        {
            car = new Car();
            car.Produce(Model);
        };

        Because of = () => car.Start();

        It shouldBeRunning = () => car.IsRunning.ShouldBe(true);

        It shouldHaveCorrectModel = () => car.Model.ShouldBe(Model);
    }

    [Subject("Car")]
    public class WhenCarIsStopped
    {
        static Car car;

        Given context = () => car = new Car();

        Because of = () => car.Stop();

        It shouldBeRunning = () => car.IsRunning.ShouldBe(false);
    }
}
