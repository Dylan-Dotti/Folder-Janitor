using System;
using Topshelf;

namespace TopshelfBoilerplate
{
    public class TopshelfService
    {
        public string ServiceName { get; private set; }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }

        public TopshelfService(string serviceName,
            string displayName, string description)
        {
            ServiceName = serviceName;
            DisplayName = displayName;
            Description = description;
        }

        public void Run(IServiceWorker runnableService)
        {
            var exitCode = HostFactory.Run(config =>
            {
                config.Service<IServiceWorker>(service =>
                {
                    service.ConstructUsing(settings => runnableService);
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });
                config.SetServiceName(ServiceName);
                config.SetDisplayName(DisplayName);
                config.SetDescription(Description);
            });
            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}
