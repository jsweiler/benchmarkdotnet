using BenchmarkDotNet.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class Concatenation
    {
        [Benchmark(Baseline = true)]
        public string ConcatenationWithPlus()
        {
            string result = string.Empty;
            for (int i = 0; i < 1000; i++)
            {
                result += i;
            }
            return result;
        }

        [Benchmark]
        public string ConcatenationWithStringBuilder()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                sb.Append(i);
            }
            return sb.ToString();
        }
    }
}
