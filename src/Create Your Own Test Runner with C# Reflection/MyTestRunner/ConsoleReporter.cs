namespace MyTestRunner
{
    using System;

    public class ConsoleReporter : ITestReporter
    {
        public void Report(string message = null)
            => Console.Write(message);

        public void ReportLine(string message = null)
            => Console.WriteLine(message);
    }
}
