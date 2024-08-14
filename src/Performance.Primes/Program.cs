namespace Performance.Primes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int limit = 1000000;
            var primes = GetPrimes2(limit);
            Console.WriteLine($"Sum of primes up to {limit}: {primes.Sum()}");
        }

        static List<long> GetPrimes(int limit)
        {
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

        static bool IsPrime(long number)
        {
            if (number < 2) return false;
            for (long i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        public static List<long> GetPrimes2(long limit)
        {
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
