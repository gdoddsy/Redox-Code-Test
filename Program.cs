using System;
using System.Collections.Generic;
using System.Linq;

namespace Redox_Code_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a list of integers from 1 to 100.
            List<int> numbers = Enumerable.Range(1,100).ToList();

            // Use LINQ to find all even numbers in the list and print them.
            List<int> evenNumbers = numbers.Where(n => n % 2 == 0).ToList();
            Console.WriteLine("All even numbers in the list: ");
            evenNumbers.ForEach(p => Console.WriteLine(p));
            
            // Use a loop to find all numbers divisible by 3 or 5, but not by 3 and 5
            Console.WriteLine("All numbers divisible by 3 or 5, but not by 3 and 5: ");
            foreach (var number in numbers)
            {
           	 	if ((number % 3 == 0 || number % 5 == 0) && !(number % 3 == 0 && number % 5 == 0))
            	{
            	    Console.WriteLine(number);
            	}
        	}

            // Event Scheduler
            Console.WriteLine("The current time is: " + System.DateTime.Now);
            EventScheduler scheduler = new EventScheduler();

            Event event1 = new Event
            {
                Name = "Event 1",
                Location = "Location 1",
                DateTime = new DateTime(2023, 6, 21, 08, 00, 0) // an event to be scheduled in the past will be unsuccessful
            };

            Event event2 = new Event
            {
                Name = "Event 2",
                Location = "Location 2",
                DateTime = new DateTime(2023, 7, 25, 22, 44, 0) // scheduling of this event will be successful
            };

            Event event3 = new Event
            {
                Name = "Event 3",
                Location = "Location 3",
                DateTime = new DateTime(2023, 7, 24, 16, 0, 0) // scheduling of this event will be successful
            };
            
            Event event4 = new Event
            {
                Name = "Event 4",
                Location = "Location 3",
                DateTime = new DateTime(2023, 7, 24, 16, 0, 0) // scheduling of this event will be unsuccessful in the first try, and successful after Event 3 has been cancelled
            };

            scheduler.ScheduleEvent(event1);
            scheduler.ScheduleEvent(event2);
            scheduler.ScheduleEvent(event3);
            scheduler.ScheduleEvent(event4);
            scheduler.CancelEvent(event3);
            scheduler.ScheduleEvent(event4);
            
            scheduler.GetUpcomingEvents();
        }
    }
}
