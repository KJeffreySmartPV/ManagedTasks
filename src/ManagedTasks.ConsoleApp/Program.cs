namespace ManagedTasks.ConsoleApp
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Steeltoe.Common.Tasks;
    using Steeltoe.Management.TaskCore;

    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, args); 
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var config = serviceProvider.GetRequiredService<IConfiguration>();
            var taskName = config.GetValue<string>("runtask");
            
            if (!FindAndRunTask(taskName, serviceProvider))
            {
                serviceProvider.GetService<App>().Run();
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection, string[] args)
        {
            serviceCollection.AddTask<HelloWorldTask>();
            serviceCollection.AddTask<MerryXmasWorldTask>();
            serviceCollection.AddTask<HappyNewYearTask>();
            serviceCollection.AddTask<GoodByeWorldTask>();

            var configuration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables()
                .Build();

            serviceCollection.AddSingleton<IConfiguration>(configuration);

            serviceCollection.AddTransient((sp) =>
            {
                var tasks = sp.GetServices<IApplicationTask>();
                return new App(tasks);
            });
        }

        private static bool FindAndRunTask(string taskName, IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope().ServiceProvider;
            if (taskName == null) return false;

            var task = scope.GetServices<IApplicationTask>().FirstOrDefault(x => x.Name.ToLower() == taskName.ToLower());
            if (task != null)
            {
                task.Run();
            }
            else
            {
                var logger = scope.GetService<ILoggerFactory>()
                    .CreateLogger("CloudFoundryTasks");
                logger.LogError($"No task with name {taskName} is found registered in service container");
            }

            return true;
        }
    }
}
