using System.Timers;

namespace Memory
{
    internal class Program
    {

        private static List<System.Timers.Timer> _timers = new List<System.Timers.Timer>();
        private static EventHandler _handler;
        private static List<SimpleObject> _objects = new List<SimpleObject>();
        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter to start...");
            Console.ReadKey();

            _handler = (sender, e) => { /* Do nothing */ };

            for (var i = 0; i < 100000; i++)
            {
                var timer = new System.Timers.Timer(1000);
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
                _timers.Add(timer);
            }
            //foreach (var obj in _objects)
            //{
            //    obj.SomeEvent -= _handler;
            //}
            _objects.Clear();

            foreach (var timer in _timers)
            {
                timer.Elapsed -= Timer_Elapsed;
                timer.Stop();
                timer.Dispose();
            }
            _timers.Clear();

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        private static void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            var obj = new SimpleObject();
            obj.SomeEvent += _handler;
            _objects.Add(obj);
        }

        class SimpleObject
        {
            public event EventHandler SomeEvent;

            public void RaiseEvent()
            {
                SomeEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
