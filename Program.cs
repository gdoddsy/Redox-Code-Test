using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redox_Code_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Exercise 1: LINQ Query

            //Create a list of integers from 1 to 100
            var numberList = new List<int>();

            for (int i = 1; i <= 100; i++)
            {
                numberList.Add(i);
            }

            Console.WriteLine("Number list");
            foreach (var item in numberList)
            {
                Console.WriteLine(item);
            }

            //Find all even numbers in the list using LINQ and print them.
            IEnumerable<int> evenNumbers =
            from num in numberList
            where num % 2 == 0
            select num;

            Console.WriteLine("List of even numbers");
            foreach (var item in evenNumbers)
            {
                Console.WriteLine(item);
            }

            //Use a loop to find all of the numbers in the original list that
            //are divisible by 3 or 5, but not 3 and 5
            IEnumerable<int> filterNumber =
            from num in numberList
            where (num % 3 == 0 || num % 5 == 0) && (num % 15 != 0)
            select num;

            Console.WriteLine("List of numbers which are divisible by 3 or 5, but not 3 and 5");
            foreach (var item in filterNumber)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
    }

    //Exercise 2: Event Scheduler

    //1. Create a class named 'Event' with properties 'Name', 'Location', 'DateTime'.
    public class Event
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime DateTime { get; set; }
    }

    //2. Create a class EventScheduler with a list of Event. Add methods to
    //   ScheduleEvent, CancelEvent, and GetUpcomingEvents.
    public class EventScheduler
    {
        public List<Event> events = new List<Event>();

        public void ScheduleEvent(string name, string location, DateTime dateTime)
        {
            IEnumerable<Event> duplicateEvent =
            from e in events
            where e.DateTime == dateTime
            select e;

            //3. Implement a feature to prevent double-booking, where two events are scheduled
            //   for the exact same time.
            if (duplicateEvent.Count() == 0)
            {
                var meetingEvent =
                new Event() { Name = name, Location = location, DateTime = dateTime };
                events.Add(meetingEvent);

                Console.WriteLine("Event created!");
            }
            else
            {
                Console.WriteLine($"Event already booked for this {dateTime} time");
            }
        }


        public void CancelEvent(string name)
        {
            events.Remove(new Event() { Name = name });

            Console.WriteLine("Event cancelled!");
        }

        public void GetUpcomingEvents()
        {
            IEnumerable<Event> upComingEvents =
            from e in events
            where e.DateTime >= DateTime.Now
            select e;

            foreach (var item in upComingEvents)
            {
                Console.WriteLine(item);
            }
        }
    }
}
