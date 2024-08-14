using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Memory
{
    public class TimerManager
    {
        private List<TimerClass> _leakyObjects = new List<TimerClass>();

        public event EventHandler TimerEvent;

        public void StartTimers()
        {
            for (int i = 0; i < 500; i++)
            {
                var leakyObject = new TimerClass();
                _leakyObjects.Add(leakyObject);
                leakyObject.StartTimer();
                TimerEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public void StopTimers()
        {
            foreach (var leakyObject in _leakyObjects)
            {
                leakyObject.Dispose();
            }
            _leakyObjects.Clear();
        }
    }
}
