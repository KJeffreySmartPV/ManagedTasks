using System;

namespace ManagedTasks.WebApp
{
    using System.IO;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;
    using Steeltoe.Extensions.Configuration.CloudFoundry;
    using Steeltoe.Management.CloudFoundry;
    using Steeltoe.Management.TaskCore;

    class Program
    {
        static void Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder()
                .AddCloudFoundry() // config
                .ConfigureLogging((context, builder) => builder.AddEventSourceLogger())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.RunWithTasks();
        }
    }
}
