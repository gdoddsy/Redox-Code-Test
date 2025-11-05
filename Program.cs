using System;
using System.Collections.Generic;
using System.Linq;
using RedoxCodeTest;

class Program
{
    static void Main()
    {
        // ==============================
        // Exercise 1: LINQ Query
        // ==============================
        Console.WriteLine("===== Exercise 1: LINQ Query =====");

        // 1) List 1..100
        var numbers = Enumerable.Range(1, 100).ToList();

        // 2) LINQ: all evens
        var evens = numbers.Where(n => n % 2 == 0).ToList();
        Console.WriteLine("Even numbers:");
        Console.WriteLine(string.Join(", ", evens));

        // 3) LOOP: divisible by 3 or 5, but NOT both (XOR)
        var special = new List<int>();
        foreach (var n in numbers)
        {
            bool div3 = n % 3 == 0;
            bool div5 = n % 5 == 0;
            if (div3 ^ div5) special.Add(n); // XOR -> one or the other
        }

        Console.WriteLine("\nDivisible by 3 or 5, but not both:");
        Console.WriteLine(string.Join(", ", special));

        // ==============================
        // Exercise 2: Event Scheduler
        // ==============================
        Console.WriteLine("\n===== Exercise 2: Event Scheduler =====");

        var scheduler = new EventScheduler("events.json");

        // start clean for a deterministic demo
        scheduler.ReplaceAll(Array.Empty<Event>());

        var now = DateTime.Now;

        var e1 = new Event { Name = "Team Standup", Location = "Sydney HQ", DateTime = now.AddHours(2) };
        var e2 = new Event { Name = "Client Call",  Location = "Zoom",      DateTime = now.AddHours(4) };
        var e3 = new Event { Name = "Code Review",  Location = "Sydney HQ", DateTime = now.AddDays(1) };

        scheduler.ScheduleEvent(e1);
        scheduler.ScheduleEvent(e2);
        scheduler.ScheduleEvent(e3);

        Console.WriteLine("Scheduled three events.");

        // attempt a double-book at the same time as e2
        try
        {
            scheduler.ScheduleEvent(new Event { Name = "Another Meeting", Location = "Teams", DateTime = e2.DateTime });
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Double-booking prevented: {ex.Message}");
        }

        Console.WriteLine("\nUpcoming events:");
        foreach (var e in scheduler.GetUpcomingEvents())
            Console.WriteLine($" - {e}");

        // cancel one to show CancelEvent + persistence works
        scheduler.CancelEvent(e1.Name, e1.DateTime);

        Console.WriteLine("\nAfter cancel:");
        foreach (var e in scheduler.GetUpcomingEvents())
            Console.WriteLine($" - {e}");

        Console.WriteLine("\nSaved to events.json (persistent).");
    }
}

