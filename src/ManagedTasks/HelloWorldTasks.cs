namespace ManagedTasks
{
    using System;
    using Steeltoe.Common.Tasks;

    public class HelloWorldTask: IApplicationTask
    {
        public void Run()
        {
            Console.WriteLine("Hello World!");
        }

        public string Name => "SayHello";
    }

    public class GoodByeWorldTask : IApplicationTask
    {
        public void Run()
        {
            Console.WriteLine("Good Bye World!");
        }

        public string Name => "SayGoodBye";
    }

    public class MerryXmasWorldTask : IApplicationTask
    {
        public void Run()
        {
            Console.WriteLine("Merry Xmas World!");
        }

        public string Name => "SayMerryXmas";
    }

    public class HappyNewYearTask : IApplicationTask
    {
        public void Run()
        {
            Console.WriteLine("Happy New Year World!");
        }

        public string Name => "SayHappyNewYear";
    }
}