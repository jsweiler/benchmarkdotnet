using BenchmarkDotNet.Running;

namespace Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<Hashes>();

            //var summary = BenchmarkRunner.Run<Loops>();

            var summary = BenchmarkRunner.Run<Primes>();

            Console.ReadKey();
        }
    }
}
