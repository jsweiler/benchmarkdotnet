using System.Timers;

namespace Memory
{

    public class TimerClass : IDisposable
    {
        private System.Timers.Timer _timer;

        public TimerClass()
        {
            _timer = new System.Timers.Timer(1000); // Set timer to tick every second
            Program.TimerManager.TimerEvent += TimerManager_TimerEvent;
        }

        private void TimerManager_TimerEvent(object? sender, EventArgs e)
        {
            Console.WriteLine("Timer setup.");
        }

        public void StartTimer()
        {
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        private void _timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Timer event fired");
        }

        public void Dispose()
        {
            Program.TimerManager.TimerEvent -= TimerManager_TimerEvent;
            _timer.Dispose();
        }
    }
}
