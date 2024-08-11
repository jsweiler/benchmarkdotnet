using System.Text;

namespace Performance.Concatenation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter to start...");
            Console.ReadKey();

            int limit = 100000;
            string result = ConcatenateNumbers2(limit);
            Console.WriteLine($"Length of concatenated string: {result.Length}");
        }

        static string ConcatenateNumbers(int limit)
        {
            string result = "";
            for (int i = 1; i <= limit; i++)
            {
                result += i.ToString();
            }
            return result;
        }

        static string ConcatenateNumbers2(int limit)
        {
            var stringBuilder = new StringBuilder();
            for (int i = 1; i <= limit; i++)
            {
                stringBuilder.Append(i);
            }
            return stringBuilder.ToString();
        }

    }
}
