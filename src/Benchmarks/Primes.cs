using BenchmarkDotNet.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class Primes
    {
        [Benchmark(Baseline = true)]
        public List<long> GetPrimes()
        {
            var limit = 10000;
            List<long> primes = new List<long>();
            for (long i = 2; i <= limit; i++)
            {
                if (IsPrime(i))
                {
                    primes.Add(i);
                }
            }
            return primes;
        }

        bool IsPrime(long number)
        {
            if (number < 2) return false;
            var sqrt = Math.Sqrt(number);
            for (long i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        [Benchmark]
        public List<long> GetPrimes2()
        {
            var limit = 10000;
            if (limit < 2)
            {
                return new List<long>();
            }

            bool[] isPrime = new bool[limit + 1];
            for (long i = 2; i <= limit; i++)
            {
                isPrime[i] = true;
            }

            for (long p = 2; p * p <= limit; p++)
            {
                if (isPrime[p])
                {
                    for (long multiple = p * p; multiple <= limit; multiple += p)
                    {
                        isPrime[multiple] = false;
                    }
                }
            }

            List<long> primes = new List<long>();
            for (long i = 2; i <= limit; i++)
            {
                if (isPrime[i])
                {
                    primes.Add(i);
                }
            }

            return primes;
        }
    }
}
