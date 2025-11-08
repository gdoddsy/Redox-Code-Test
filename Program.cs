using System;
using System.Collections.Generic;
using System.Linq;

namespace Redox_Code_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Exercise 1: LINQ Query \n");
            Exercise1();

            Console.WriteLine("\nExercise 2: Event Scheduler \n");
            Exercise2();
        }

        /// <summary>
        /// Exercise 1: Working with LINQ queries and loops
        /// </summary>
        static void Exercise1()
        {
            // Step 1: Create a list of integers from 1 to 100
            // We use Enumerable.Range to generate numbers from 1 to 100
            List<int> numbers = Enumerable.Range(1, 100).ToList();
            Console.WriteLine($"Created a list with numbers from 1 to 100 (total: {numbers.Count} numbers)");

            // Step 2: Use LINQ to find all even numbers
            // LINQ allows us to query collections easily
            // Where() filters the list, keeping only numbers where the condition is true
            // x % 2 == 0 checks if a number is even (divisible by 2 with no remainder)
            var evenNumbers = numbers.Where(x => x % 2 == 0).ToList();
            
            Console.WriteLine("\nEven numbers found using LINQ:");
            Console.WriteLine(string.Join(", ", evenNumbers));
            Console.WriteLine($"Total even numbers: {evenNumbers.Count}");

            // Step 3: Find numbers divisible by 3 OR 5, but NOT both
            // We'll use a loop to demonstrate an alternative approach
            // A number is divisible by 3 OR 5 if: (x % 3 == 0) OR (x % 5 == 0)
            // But we need to exclude numbers divisible by BOTH 3 AND 5
            // So: (divisible by 3 OR 5) AND (NOT divisible by 15)
            List<int> divisibleBy3Or5 = new List<int>();

            // Loop through each number in the original list
            foreach (int number in numbers)
            {
                // Check if number is divisible by 3
                bool divisibleBy3 = (number % 3 == 0);
                
                // Check if number is divisible by 5
                bool divisibleBy5 = (number % 5 == 0);
                
                // Check if number is divisible by both 3 and 5 (i.e., divisible by 15)
                bool divisibleByBoth = (number % 15 == 0);

                // Add to list if: (divisible by 3 OR 5) AND (NOT divisible by both)
                if ((divisibleBy3 || divisibleBy5) && !divisibleByBoth)
                {
                    divisibleBy3Or5.Add(number);
                }
            }

            Console.WriteLine("\nNumbers divisible by 3 OR 5 (but not both), found using a loop:");
            Console.WriteLine(string.Join(", ", divisibleBy3Or5));
            Console.WriteLine($"Total numbers: {divisibleBy3Or5.Count}");
        }

        /// <summary>
        /// Exercise 2: Event Scheduler
        /// </summary>
        static void Exercise2()
        {
            // Create an instance of the EventScheduler
            // This will manage our events and handle scheduling
            EventScheduler scheduler = new EventScheduler();

            Console.WriteLine("Scheduling events...\n");

            // Try to schedule first event
            Event event1 = new Event
            {
                Name = "Team Meeting",
                Location = "Conference Room A",
                DateTime = new DateTime(2024, 12, 15, 10, 0, 0) // December 15, 2024 at 10:00 AM
            };
            scheduler.ScheduleEvent(event1);
            Console.WriteLine($"Event 1 Scheduled: {event1.Name} at {event1.DateTime}");

            // Try to schedule second event at a different time
            Event event2 = new Event
            {
                Name = "Client Presentation",
                Location = "Main Hall",
                DateTime = new DateTime(2024, 12, 15, 14, 0, 0) // December 15, 2024 at 2:00 PM
            };
            scheduler.ScheduleEvent(event2);
            Console.WriteLine($"Event 2 Scheduled: {event2.Name} at {event2.DateTime}");

            // Try to schedule an event at the same time (should fail due to double-booking prevention)
            Event event3 = new Event
            {
                Name = "Another Meeting",
                Location = "Room B",
                DateTime = new DateTime(2024, 12, 15, 10, 0, 0) // Same time as event1
            };
            bool scheduled = scheduler.ScheduleEvent(event3);
            if (!scheduled)
            {
                Console.WriteLine($"Event 3 Could not schedule: {event3.Name} - Time slot already booked!");
            }

            // Display upcoming events
            Console.WriteLine("\nUpcoming events:");
            var upcomingEvents = scheduler.GetUpcomingEvents();
            foreach (var evt in upcomingEvents)
            {
                Console.WriteLine($"  - {evt.Name} at {evt.Location} on {evt.DateTime}");
            }

            // Example: Cancel an event
            Console.WriteLine($"\nCancelling event: {event1.Name}");
            scheduler.CancelEvent(event1.Name);
            Console.WriteLine("Event cancelled successfully.");

            // Display remaining upcoming events
            Console.WriteLine("\nRemaining upcoming events:");
            upcomingEvents = scheduler.GetUpcomingEvents();
            if (upcomingEvents.Count == 0)
            {
                Console.WriteLine("  No upcoming events.");
            }
            else
            {
                foreach (var evt in upcomingEvents)
                {
                    Console.WriteLine($"  - {evt.Name} at {evt.Location} on {evt.DateTime}");
                }
            }
        }
    }
}
