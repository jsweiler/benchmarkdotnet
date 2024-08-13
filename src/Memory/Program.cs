using System.Timers;

namespace Memory
{
    internal class Program
    {
        public static TimerManager TimerManager { get; private set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter to start...");
            Console.ReadKey();

            var manager = new TimerManager();
            TimerManager = manager;
            manager.StartTimers();

            Console.WriteLine("Press Enter to stop...");
            Console.ReadKey();
            manager.StopTimers();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Optionally, force a second collection to ensure finalizers are collected
            GC.Collect();


            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        
    }
}
