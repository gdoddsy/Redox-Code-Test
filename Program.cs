using System;
using System.Collections.Generic;

namespace Redox_Code_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // EXERCISE 1

            // step 1
            List<int> numbers = new List<int>();
            for (int i = 1; i <= 100; i++)
            {
                numbers.Add(i);
            }

            // step 2
            List<int> evenNumbers = numbers.Where(n => n % 2 == 0).ToList();

            foreach (int num in evenNumbers)
            {
                Console.WriteLine(num);
            }

            // step 3
            List<int> divisibleNumbers = new List<int>();

            foreach (int num in numbers)
            {
                if ((num % 3 == 0 || num % 5 == 0) && !(num % 3 == 0 && num % 5 == 0))
                {
                    divisibleNumbers.Add(num);
                }
            }

            foreach (int num in divisibleNumbers)
            {
                Console.WriteLine(num);
            }
            
        }
    }

    // EXERCISE 2

    // step 1
    public class Event
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime DateTime { get; set; }
    }

    // step 2
    public class EventScheduler
    {
        private List<Event> events;

        public void ScheduleEvent(Event newEvent)
        {
            // step 3
            if (events.Any(e => e.DateTime == newEvent.DateTime))
            {
                Console.WriteLine("Cannot book event, timeslot already booked.");
                return;
            }
            
            events.Add(newEvent);
        }

        public void CancelEvent(Event eventToCancel)
        {
            events.Remove(eventToCancel);
        }

        public List<Event> GetUpcomingEvents()
        {
            return events;
        }
    }
}
