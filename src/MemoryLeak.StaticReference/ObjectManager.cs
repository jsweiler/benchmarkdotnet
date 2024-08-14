using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryLeak.StaticReference
{
    public class ObjectManager
    {
        private List<LeakyClass> data = new List<LeakyClass>();
        public void CreateObjects()
        {
            for (int i = 0; i < 100000; i++)
            {
                var leakyObject = new LeakyClass(i);
                //DataStore.Add(leakyObject);
                data.Add(leakyObject);
            }
        }
    }
}
