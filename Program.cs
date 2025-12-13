using System;
using System.Collections.Generic;
using System.Linq;
using Redox_Code_Test.Models;
using Redox_Code_Test.Services;
using Redox_Code_Test.Tests;

namespace Redox_Code_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Refactor to run in CLI mode with the following options:
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: testexercise1|testexercise2|schedule|cancel|getupcomingevents");
                return;
            }
            //
            var scheduler = new EventScheduler();

            switch (args[0])
            {
                case "testexercise1":
                    RunExercise1();
                    break;

                case "testexercise2":
                    TestEventScheduler.RunTests();
                    break;

                case "schedule":
                    if (args.Length < 4)
                    {
                        Console.WriteLine("Usage: schedule <name> <location> <datetime>");
                        return;
                    }
                    scheduler.ScheduleEvent(new Event
                    {
                        Name = args[1],
                        Location = args[2],
                        DateTime = DateTime.Parse(args[3])
                    });
                    Console.WriteLine($"Scheduled: {args[1]}");
                    break;

                case "cancel":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Usage: cancel <name>");
                        return;
                    }
                    scheduler.CancelEvent(args[1]);
                    Console.WriteLine($"Cancelled: {args[1]}");
                    break;

                case "getupcomingevents":
                    var events = scheduler.GetUpcomingEvents();
                    if (events.Count == 0)
                    {
                        Console.WriteLine("No upcoming events.");
                    }
                    else
                    {
                        foreach (var e in events)
                            Console.WriteLine($"{e.Name} - {e.Location} - {e.DateTime}");
                    }
                    break;

                default:
                    Console.WriteLine($"Unknown command: {args[0]}");
                    Console.WriteLine("Usage: testexercise1|testexercise2|schedule|cancel|getupcomingevents");
                    break;
            }
        }

        static void RunExercise1()
        {
            //use LINQ Enumerable to create integers 1-100
            IEnumerable<int> numbers = Enumerable.Range(1, 100);
            //create even variables where the remainder of division by 2 is zero
            var evenNumbers = numbers.Where(n => n % 2 == 0);
            //print out string with comma separator
            Console.WriteLine("Even numbers:");
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
            Console.WriteLine("Divisible numbers (3 OR 5, NOT 15)");
            Console.WriteLine(string.Join(", ", divisibleNumbers));
        }
    }
}
