
using System.Timers;

namespace Memory
{
    public class LeakyClass
    {
        private System.Timers.Timer _timer;

        public LeakyClass()
        {
            _timer = new System.Timers.Timer(1000); // Set timer to tick every second
        }

        public void StartTimer()
        {
            _timer.Elapsed += OnTimedEvent;
            _timer.Start();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Timer event fired");
        }
    }
}
