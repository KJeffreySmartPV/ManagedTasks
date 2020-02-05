namespace ManagedTasks
{
    using System.Threading;
    using Steeltoe.Common.Tasks;

    public class DelayApplicationTaskDecorator<TApplicationTask> : IApplicationTask 
        where TApplicationTask : class, IApplicationTask, new()
    {
        private TApplicationTask innerTask = new TApplicationTask();

        public string Name => innerTask.Name;

        public void Run()
        {
            Thread.Sleep(10000);
            innerTask.Run();
        }
    }
}