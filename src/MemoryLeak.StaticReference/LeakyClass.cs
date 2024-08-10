using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryLeak.StaticReference
{
    public class LeakyClass
    {
        public int Id { get; }

        public LeakyClass(int id)
        {
            Id = id;
        }

        ~LeakyClass()
        {
            Console.WriteLine($"LeakyClass with Id {Id} is being finalized.");
        }
    }

    public static class LeakyClassStore
    {
        private static List<LeakyClass> _leakyObjects = new List<LeakyClass>();

        public static void Add(LeakyClass leakyObject)
        {
            _leakyObjects.Add(leakyObject);
        }
    }
}
