namespace ProcessManipulationCSharp
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    public class Program
    {
        public static void Main(string[] args)
        {
            // Get processes
            var chromes = Process.GetProcessesByName("chrome");

            Console.WriteLine(chromes.Length);

            // Open process and wait for finish
            var processInfo = new ProcessStartInfo
            {
                FileName = "sample.txt",
                WindowStyle = ProcessWindowStyle.Maximized
            };

            using (var process = Process.Start(processInfo))
            {
                Thread.Sleep(3000);

                process?.Kill();
            }

            // Open process and redirect output - make sure you build the whole solution first
            var outputProcessInfo = new ProcessStartInfo
            {
                FileName = @"C:\Data\Projects\My Tested ASP.NET\Source\MyTested.AspNet.TV\src\Process Manipulation With C#\ConsoleApp\bin\Debug\ConsoleApp.exe",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            Console.WriteLine("Starting Child Process");
            Console.WriteLine(new string('-', 20));


            using (var process = Process.Start(outputProcessInfo))
            {
                using (var reader = process?.StandardOutput)
                {
                    Console.WriteLine(reader?.ReadToEnd());
                }
            }

            Console.WriteLine(new string('-', 20));
            Console.WriteLine("Ending Child Process");
        }
    }
}
