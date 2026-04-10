using Redox_Code_Test.Models;
using Redox_Code_Test.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Redox_Code_Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Entry point of application
            ShowMainMenu();
        }

        /// <summary>
        /// Displays the main menu and routes user selection
        /// </summary>
        static void ShowMainMenu()
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1: Numbers Task");
            Console.WriteLine("2: Event Scheduler");
            Console.WriteLine("3: Exit");
            Console.Write("Choose an option: ");

            string selectedOption = Console.ReadLine();

            switch (selectedOption)
            {
                case "1":
                    NumbersTask();
                    break;

                case "2":
                    RunEventScheduler();
                    break;

                case "3":
                    Console.WriteLine("Exiting...");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        /// <summary>
        /// Performs number-related operations using LINQ and loops
        /// </summary>
        static void NumbersTask()
        {
            // Generate numbers from 1 to 100
            var numbers = Enumerable.Range(1, 100);

            // Get even numbers using LINQ
            var evenNumbers = numbers.Where(n => n % 2 == 0);

            Console.WriteLine("\nEven numbers:");
            Console.WriteLine(string.Join(", ", evenNumbers));

            // Numbers divisible by 3 OR 5 but NOT both
            var filteredNumbers = numbers
                .Where(n => (n % 3 == 0) ^ (n % 5 == 0)); // XOR simplifies condition

            Console.WriteLine("\nDivisible by 3 or 5 but not both:");
            Console.WriteLine(string.Join(", ", filteredNumbers));
        }

        /// <summary>
        /// Handles Event Scheduler menu loop
        /// </summary>
        static void RunEventScheduler()
        {
            var scheduler = new EventScheduler();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nEvent Scheduler Menu:");
                Console.WriteLine("1: View Upcoming Events");
                Console.WriteLine("2: Schedule an Event");
                Console.WriteLine("3: Cancel an Event");
                Console.WriteLine("4: Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowUpcomingEvents(scheduler);
                        break;

                    case "2":
                        ScheduleEvent(scheduler);
                        break;

                    case "3":
                        CancelEvent(scheduler);
                        break;

                    case "4":
                        exit = true;
                        Console.WriteLine("Exiting Event Scheduler...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Displays all upcoming events
        /// </summary>
        static void ShowUpcomingEvents(EventScheduler scheduler)
        {
            var events = scheduler.GetUpcomingEvents();

            if (!events.Any())
            {
                Console.WriteLine("No events found.");
                return;
            }

            Console.WriteLine("\nUpcoming Events:");
            foreach (var e in events)
            {
                Console.WriteLine($"{e.Name} at {e.Location} on {e.DateTime}");
            }
        }

        /// <summary>
        /// Collects input and schedules a new event
        /// </summary>
        static void ScheduleEvent(EventScheduler scheduler)
        {
            Console.Write("Enter Event Name: ");
            string name = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Enter Event Location: ");
            string location = Console.ReadLine()?.Trim() ?? string.Empty;

            // Validate date input
            DateTime dateTime = ReadValidDateTime();

            var newEvent = new Event
            {
                Name = name,
                Location = location,
                DateTime = dateTime
            };

            // Try scheduling event
            if (scheduler.ScheduleEvent(newEvent))
            {
                Console.WriteLine("Event scheduled successfully.");
            }
            else
            {
                Console.WriteLine("Event already booked for this time.");
            }
        }

        /// <summary>
        /// Handles event cancellation
        /// </summary>
        static void CancelEvent(EventScheduler scheduler)
        {
            Console.Write("Enter Event Name to Cancel: ");
            string eventName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(eventName))
            {
                Console.WriteLine("Invalid event name.");
                return;
            }

            if (!scheduler.CancelEvent(eventName))
            {
                Console.WriteLine("Failed to cancel event. Event not found.");
            }
            else
            {
                Console.WriteLine("Event cancelled successfully.");
            }
        }

        /// <summary>
        /// Reads and validates DateTime input from user
        /// </summary>
        static DateTime ReadValidDateTime()
        {
            while (true)
            {
                Console.Write("Enter Event Date and Time (yyyy-MM-dd HH:mm): ");
                string input = Console.ReadLine();

                if (DateTime.TryParse(input, out DateTime dateTime))
                {
                    return dateTime;
                }

                Console.WriteLine("Invalid format. Please use: yyyy-MM-dd HH:mm");
            }
        }
    }
}