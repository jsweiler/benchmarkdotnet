namespace MemoryLeak.StaticReference
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter to start...");
            Console.ReadKey();

            var manager = new ObjectManager();
            manager.CreateObjects();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Optionally, force a second collection to ensure finalizers are collected
            GC.Collect();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
