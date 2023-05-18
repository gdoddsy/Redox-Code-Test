using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Redox_Code_Test
{
    /// <summary>
    /// Main Program
    /// </summary>
    class Program
    {
        /// <summary>
        /// List containing the desired numbers
        /// </summary>
        private static List<int> numList;

        /// <summary>
        /// The main function for the application
        /// </summary>
        /// <param name="args">Arguments for the application</param>
        static void Main(string[] args)
        {
            numList = listPopulate(1, 99);
            /* Even Numbers */
            retrieveEvenNumbers(numList).ToList().ForEach(i =>
            {
                Console.Write($"{i} ");
            });
            Console.WriteLine();
            /* Divisible by three or five but not both */
            retrieveDivisibleBy(numList).ToList().ForEach(i =>
            {
                Console.Write($"{i} ");
            });
        }

        /// <summary>
        /// Populate the list of numbers
        /// </summary>
        /// <param name="start">The number to start at</param>
        /// <param name="ammount">The total numbers to generate</param>
        /// <returns>The list of numbers</returns>
        private static List<int> listPopulate(int start, int ammount)
        {
            return Enumerable.Range(start, ammount).ToList();
        }

        /// <summary>
        /// Retrieve a list of the event numbers from the provides list
        /// </summary>
        /// <param name="list">List of numbers to scan</param>
        /// <returns>List of found event numbers</returns>
        private static IEnumerable<int> retrieveEvenNumbers(List<int> list)
        {
             return list.Select((n) => new { Number = n })
                .Where(x => x.Number % 2 == 0)
                .Select(x => x.Number);
        }

        /// <summary>
        /// Retrieve a list of numbers from the provided list that are divisible by three or five but not both.
        /// </summary>
        /// <param name="list">List of numbers to scan</param>
        /// <returns>List of divisible numbers</returns>
        private static IEnumerable<int> retrieveDivisibleBy(List<int> list)
        {
            return list.Select((n) => new { Number = n })
                .Where(x => (
                    ((x.Number % 3 == 0) || (x.Number % 5 == 0)) &&
                    !((x.Number % 3 == 0) && (x.Number % 5 == 0))
                 ))
                .Select(x => x.Number);
                
        }
    }
}
