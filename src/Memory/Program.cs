using System.Timers;

namespace Memory
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter to start...");
            Console.ReadKey();

            var manager = new TimerManager();
            manager.StartTimers();

            Console.WriteLine("Press Enter to stop...");
            Console.ReadKey();
            manager.StopTimers();


            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        
    }
}
