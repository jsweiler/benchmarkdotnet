using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    public class TimerManager
    {
        private List<TimerClass> _leakyObjects = new List<TimerClass>();

        public void StartTimers()
        {
            for (int i = 0; i < 100000; i++)
            {
                var leakyObject = new TimerClass();
                _leakyObjects.Add(leakyObject);
                leakyObject.StartTimer();
            }
        }

        public void StopTimers()
        {
            foreach (var leakyObject in _leakyObjects)
            {
                leakyObject.Dispose();
            }
        }
    }
}
