using BenchmarkDotNet.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class Loops
    {
        int[] numbers;

        [GlobalSetup]
        public void Setup()
        {
            numbers = Enumerable.Range(1, 1000).ToArray();
        }

        [Benchmark(Baseline = true)]
        public void SumForEach()
        {
            var sum = 0;
            foreach (var n in numbers)
            {
                sum += n;
            }
        }

        [Benchmark]
        public void SumForEachWithIL()
        {
            var numbers = new int[] { 1, 2, 3, 4, 5 };

            var method = new DynamicMethod("SumNumbers", typeof(int), new Type[] { typeof(int[]) }, typeof(Program).Module);
            var il = method.GetILGenerator();

            // Local variable for the sum
            il.DeclareLocal(typeof(int)); // sum
                                          // Local variable for the loop index
            il.DeclareLocal(typeof(int)); // i

            // Initialize sum to 0
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Stloc_0);

            // Initialize i to 0
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Stloc_1);

            // Define labels for the loop control
            var loopCheck = il.DefineLabel();
            var loopStart = il.DefineLabel();

            // Jump to the loop check
            il.Emit(OpCodes.Br_S, loopCheck);

            // Start of the loop
            il.MarkLabel(loopStart);

            // Load sum
            il.Emit(OpCodes.Ldloc_0);

            // Load numbers[i]
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_1);
            il.Emit(OpCodes.Ldelem_I4);

            // Add numbers[i] to sum
            il.Emit(OpCodes.Add);

            // Store the result back in sum
            il.Emit(OpCodes.Stloc_0);

            // Increment i
            il.Emit(OpCodes.Ldloc_1);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc_1);

            // Loop check
            il.MarkLabel(loopCheck);

            // Load i
            il.Emit(OpCodes.Ldloc_1);

            // Load numbers.Length
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldlen);
            il.Emit(OpCodes.Conv_I4);

            // Compare i < numbers.Length
            il.Emit(OpCodes.Blt_S, loopStart);

            // Load the final sum
            il.Emit(OpCodes.Ldloc_0);

            // Return the sum
            il.Emit(OpCodes.Ret);

            // Create a delegate and invoke the method
            var sumNumbers = (Func<int[], int>)method.CreateDelegate(typeof(Func<int[], int>));
            var sum = sumNumbers(numbers);
        }

        [Benchmark]
        public void SumFor()
        {
            var sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }
        }
        // why? see https://www.c-sharpcorner.com/article/c-sharp-performance-of-code-for-loop-vs-for-each-loop/
        // https://sharplab.io/#v2:CYLg1APgAgDABFAjAOgDIEsB2BHA3HAWAChiA3AQwCc5MBXAWwCMBTSgZzgF44BRO+1uUYAbZsgBK5TAHNmACkQAaOIhhqAlMgAqAewCClSuQCec9bmJkqcNgwBiOyj3IBjABZc4MC0QBmj5lcPOQpqTDgsGgYWdnViAG9iOGSbe0dndzgwbkwfAF9LIlDU+gdqbm9if2o5LAAXCM9vRoAeKKZWNjRmGTq3fHQwMDiiRKIUkrKsnOjOgG10AF18oA===

        [Benchmark]
        public void SumLINQ()
        {
            var sum = numbers.Sum();
        }

        [Benchmark]
        public void SumSpanForSIMD()
        {
            // cast the array to vectors without copying
            var numbersSpan = numbers.AsSpan();
            var vectors = MemoryMarshal.Cast<int, Vector<int>>(numbersSpan);

            // add each vector to the sum vector
            var sumVector = Vector<int>.Zero;
            foreach (ref readonly var vector in vectors)
            {
                sumVector += vector;
            }

            // compute
            var sum = Vector.Sum(sumVector);

            // find what elements of the source were left out
            var remainder = numbersSpan.Length % Vector<int>.Count;
            numbersSpan = numbersSpan[^remainder..];
            foreach (ref readonly var value in numbersSpan)
            {
                sum += value;
            }

            // adapted from https://antao-almada.medium.com/single-instruction-multiple-data-simd-in-net-393b8cf9a90
        }
    }
}
