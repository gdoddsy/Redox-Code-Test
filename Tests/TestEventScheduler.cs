using System;
using System.IO;
using Redox_Code_Test.Models;
using Redox_Code_Test.Services;


namespace Redox_Code_Test.Tests
{
    public class TestEventScheduler
    {
        public static void RunTests()
        {
            //delete persistantEvents json
            if (File.Exists("persistantEvents.json")) File.Delete("persistantEvents.json");

            //use a new scheduler with two DateTimes in  the future
            var scheduler = new EventScheduler();
            var time1 = DateTime.Now.AddDays(1).Date.AddHours(4);
            var time2 = DateTime.Now.AddDays(1).Date.AddHours(11);

            // Test scheduling
            scheduler.ScheduleEvent(new Event { Name = "Meeting", DateTime = time1 });
            scheduler.ScheduleEvent(new Event { Name = "Lunch", DateTime = time2 });
            //should have two upcoming events
            Test("Schedule events", scheduler.GetUpcomingEvents().Count == 2);

            // Test conflict detection - schedule event with same time as time1
            try { scheduler.ScheduleEvent(new Event { Name = "ConflictEvent", DateTime = time1 }); Test("Conflict detection", false); }
            catch { Test("Conflict detection", true); }

            // Test cancel
            scheduler.CancelEvent("Meeting");
            //should have one event when we cancel meeting
            Test("Cancel event", scheduler.GetUpcomingEvents().Count == 1);
            //should still have one event when we create a new instancer of eventscheduler
            Test("Cancel persisted", new EventScheduler().GetUpcomingEvents().Count == 1);

            Console.WriteLine("\nDone.");
        }

        static void Test(string name, bool passed) =>
            Console.WriteLine($"{(passed ? "pass" : "fail")} {name}");
    }
}
