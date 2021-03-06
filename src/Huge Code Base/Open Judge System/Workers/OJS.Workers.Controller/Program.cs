[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace OJS.Workers.Controller
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.ServiceProcess;

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the service.
        /// </summary>
        private static void Main()
        {
            try
            {
                // Explicitly set App.config file location to prevent confusion
                // ReSharper disable once AssignNullToNotNullAttribute
                Environment.CurrentDirectory = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", "OJS.Workers.Controller.exe.config");

                // Run the service
                var servicesToRun = new ServiceBase[] { new ControllerService() };
                ServiceBase.Run(servicesToRun);
            }
            catch (Exception exception)
            {
                const string Source = "OJS.Workers.Controller";
                if (!EventLog.SourceExists(Source))
                {
                    EventLog.CreateEventSource(Source, "Application");
                }

                EventLog.WriteEntry(Source, exception.ToString(), EventLogEntryType.Error);
            }
        }
    }
}
