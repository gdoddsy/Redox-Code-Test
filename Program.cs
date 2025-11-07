﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Redox_Code_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Main menu loop
            bool running = true;

            while (running)
            {
                //Main menu options
                Console.WriteLine("Select an option:");
                Console.WriteLine("1: Event Scheduler");
                Console.WriteLine("2: Counting Problem");
                Console.WriteLine("3: Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                //user input
                switch (choice)
                {
                    case "1":
                        RunEventScheduler();
                        break;
                    case "2":
                        CountingProblem.RunCountingProblem();
                        break;
                    case "3":
                        running = false;
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void RunEventScheduler()
        {
            //Create event scheduler instance
            var scheduler = new EventScheduler();
            bool running = true;

            while (running)
            {
                //Event scheduler menu options
                Console.WriteLine("\nEvent Scheduler Menu:");
                Console.WriteLine("1: Schedule an Event");
                Console.WriteLine("2: Cancel an Event");
                Console.WriteLine("3: View Upcoming Events");
                Console.WriteLine("4: Exit");
                string choice = Console.ReadLine();

                //User input
                switch (choice)
                {
                    case "1":
                        ScheduleEvent(scheduler);
                        break;
                    case "2":
                        CancelEvent(scheduler);
                        break;
                    case "3":
                        ViewUpcomingEvents(scheduler);
                        break;
                    case "4":
                        running = false;
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            static void ScheduleEvent(EventScheduler scheduler)
            {
                //Prompt user for event details
                Console.Write("Enter Event Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Event Location: ");
                string location = Console.ReadLine();

                Console.Write("Enter Event Date and Time (yyyy-MM-dd HH:mm): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dateTime))
                {
                    //Create new event and attempt to schedule it
                    var newEvent = new Event
                    {
                        Name = name,
                        Location = location,
                        DateTime = dateTime
                    };

                    if (!scheduler.ScheduleEvent(newEvent))
                    {
                        Console.WriteLine("Failed to schedule event. Event already booked for this time.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date and time format.");
                }
            }

            static void CancelEvent(EventScheduler scheduler)
            {
                //Prompt user for event name to cancel
                Console.Write("Enter Event Name to Cancel: ");
                string eventName = Console.ReadLine();

                //Attempt to cancel the event
                if (!scheduler.CancelEvent(eventName))
                {
                    Console.WriteLine("Failed to cancel event. Event not found.");
                }
            }

            static void ViewUpcomingEvents(EventScheduler scheduler)
            {
                //Display all scheduled events
                var allEvents = scheduler.GetAllEvents(); 
                if (allEvents.Count == 0)
                {
                    Console.WriteLine("No events found.");
                }
                else
                {
                    Console.WriteLine("\nAll Events:");
                    foreach (var e in allEvents)
                    {
                        Console.WriteLine($"{e.Name} at {e.Location} on {e.DateTime}");
                    }
                }
            }


        }

    }
    public static class CountingProblem
        {
            public static void RunCountingProblem()
            {
                //List of ints from 1 to 100
                List<int> numbers = Enumerable.Range(1, 100).ToList();

                //LINQ used to find all even numbers within the list and print them
                var evenNum = numbers.Where(n => n % 2 == 0);
                Console.WriteLine("Even Numbers:");
                foreach (var even in evenNum)
                {
                    Console.WriteLine(even);
                }

                //Using a loop to find all numbers divisible by 3 or 5 but not both
                var divisBy3Or5 = new List<int>();
                foreach (var number in numbers)
                {
                    if ((number % 3 == 0 || number % 5 == 0) && !(number % 3 == 0 && number % 5 == 0))
                    {
                        divisBy3Or5.Add(number);
                    }
                }

                //Results to console
                Console.WriteLine("\nNumbers Divisible by 3 or 5 but not both:");
                Console.WriteLine(string.Join(", ", divisBy3Or5));
            }
        }
}
    