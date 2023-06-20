using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using static System.Formats.Asn1.AsnWriter;

namespace Redox_Code_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a list of integers from 1 to 100.
            List<int> NumberList = Enumerable.Range(1, 100).ToList();

            //Use LINQ to find all even numbers in the list and print them.
            List<int> SumNumbers = Enumerable.Range(1, 100).Where(n => n % 2 == 0).ToList();
            Console.WriteLine(SumNumbers);

            //Use a loop to find all of the numbers in the original list that are divisible by 3 or 5, but not 3 and 5.The result should be:
            //3, 5, 6, 9, 10, 12, 18, 20, 21, 24, 25, 27, 33, 35, 36, 39, 40, 42, 48, 50, 51, 54, 55, 57, 63, 65, 66, 69, 70, 72, 78, 80, 81, 84, 85, 87, 93, 95, 96, 99

            List<int> divisbleList = new List<int>() ;

            for (int i = 1; i < 100; i++)
            {
                if (i % 3 == 0 && i % 5 == 0) continue;
                else if (i % 3 == 0) divisbleList.Add(i);
                else if (i % 5 == 0) divisbleList.Add(i);
            }

            Console.WriteLine(divisbleList);




            // Event Scheduler
            Event event1 = new Event("Event1", "Sydney", (Convert.ToDateTime(DateTime.Now.ToShortDateString())));
            Event event2 = new Event("Event2", "Sydney", (Convert.ToDateTime(DateTime.Now.ToShortDateString())));
            EventScheduler.Instance.ScheduleEvent(0, event1);
            EventScheduler.Instance.ScheduleEvent(1, event2);
            EventScheduler.Instance.GetUpcomingEvents();
            EventScheduler.Instance.CancelEvent(event2);

        }
    }
}
