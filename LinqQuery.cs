using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redox_Code_Test
{
    public class LinqQuery
    {
        public static void EvenList(int[] intList)
        {            
            var evenIntList = intList.Where(x => x % 2 == 0).ToList();
            Console.WriteLine("1 -The list of all even integers from 1 to 100 :");
            foreach (var each in evenIntList)
            {
                if (each == 100)
                {
                    Console.Write(each);
                }
                else
                {
                    Console.Write(each + ",");
                }
            }
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------");
            
        }

        public static void DivisibleList(int[] intList)
        {
            Console.WriteLine("2 -The list of all integers from 1 to 100 that are divisible by 3 or 5, but not 3 and 5:");
            foreach (var each in intList)
            {
                if (each % 3 == 0 || each % 5 == 0)
                {
                    if (each % 15 != 0)
                    {
                        if (each == 100)
                        {
                            Console.Write(each);
                        }
                        else
                        {
                            Console.Write(each + ",");
                        }

                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------");

        }
    }
}
