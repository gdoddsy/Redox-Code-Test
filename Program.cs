using System;
using System.Collections.Generic;

namespace Redox_Code_Test
{
    class Program
    {
        public static void Main()
        {
            List<int> sequence = InitializeSequence(100);
            IEnumerable<int> evenNums = GetEvenNums(sequence);
            Console.WriteLine(string.Join(' ', evenNums));

            IEnumerable<int> multiplesOf35 = GetMultiplesOf35(sequence);
            Console.WriteLine(string.Join(' ', multiplesOf35));
        }

        public static List<int> InitializeSequence(int length)
        {
            List<int> sequence = new List<int>();
            if (length < 1)
                return sequence;

            for (int i = 1; i <= length; i++)
            {
                sequence.Add(i);
            }

            return sequence;
        }

        public static IEnumerable<int> GetEvenNums(List<int> array)
        {
            if (array == null) return array;
            return array.Where(i => i % 2 == 0);
        }

        public static IEnumerable<int> GetMultiplesOf35(List<int> array)
        {
            if (array == null) return array;
            var results = from i in array
                          where (i % 3 == 0 || i % 5 == 0) && i % 15 != 0
                          select i;
            return results;
        }
    }
}