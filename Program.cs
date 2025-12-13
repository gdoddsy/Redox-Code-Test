using System;
using System.Collections.Generic;

//import LINQ for list queries
using System.Linq;
using Redox_Code_Test.Tests;


namespace Redox_Code_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Excersise 1
            //use LINQ Enumerable to create integers 1-100
            IEnumerable<int> numbers = Enumerable.Range(1, 100);
            //create even variables where the remainder of division by 2 is zero
            var evenNumbers = numbers.Where(n => n % 2 == 0);
            //print out string with comma seperator
            Console.WriteLine(string.Join(", ", evenNumbers));
            
            //use LINQ query to find numbers divisible by 3 or 5 but NOT 15 (3 and 5) 
            //var divisibleNumbers = numbers.Where(n => (n % 3 == 0 || n % 5 == 0 ) && n % 15 != 0);

            //README requirements specify loop, using loop:
            var divisibleNumbers = new List<int>();
            foreach (var n in numbers)
            {
                if ((n % 3 == 0 || n % 5 == 0) && n % 15 != 0)
                {
                    divisibleNumbers.Add(n);
                }
            }
            //note: 100 is included and should be in README example
            Console.WriteLine(string.Join(", ", divisibleNumbers));

            // run event scheduler tests
            TestEventScheduler.RunTests();

        }
    }
}
