using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace Redox_Code_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Redox Code Test Solutions ===\n");

            // Exercise 1: LINQ Query
            Exercise1_LinqQuery();

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            // Exercise 2: Event Scheduler
            Exercise2_EventScheduler();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        #region Exercise 1: LINQ Query

        static void Exercise1_LinqQuery()
        {
            Console.WriteLine("Exercise 1: LINQ Query\n");

            // 1. Create a list of integers from 1 to 100
            List<int> numbers = Enumerable.Range(1, 100).ToList();

            // 2. Use LINQ to find all even numbers
            var evenNumbers = numbers.Where(n => n % 2 == 0).ToList();
            Console.WriteLine("Even numbers using LINQ:");
            Console.WriteLine(string.Join(", ", evenNumbers));
            Console.WriteLine();

            // 3. Use a loop to find numbers divisible by 3 or 5, but not both
            List<int> result = new List<int>();
            
            foreach (int number in numbers)
            {
                bool divisibleBy3 = number % 3 == 0;
                bool divisibleBy5 = number % 5 == 0;

                // XOR logic: divisible by 3 OR 5, but NOT both
                if (divisibleBy3 ^ divisibleBy5)
                {
                    result.Add(number);
                }
            }

            Console.WriteLine("Numbers divisible by 3 or 5 (but not both):");
            Console.WriteLine(string.Join(", ", result));
        }

        #endregion

        #region Exercise 2: Event Scheduler

        static void Exercise2_EventScheduler()
        {
            Console.WriteLine("Exercise 2: Event Scheduler\n");

            // Create an instance of the event scheduler
            EventScheduler scheduler = new EventScheduler();

            // Load any existing events from storage
            scheduler.LoadEvents();

            // Demo: Schedule some events
            try
            {
                scheduler.ScheduleEvent("Team Meeting", "Conference Room A", new DateTime(2025, 11, 10, 10, 0, 0));
                scheduler.ScheduleEvent("Client Presentation", "Boardroom", new DateTime(2025, 11, 10, 14, 30, 0));
                scheduler.ScheduleEvent("Code Review", "Dev Room", new DateTime(2025, 11, 11, 11, 0, 0));
                
                Console.WriteLine("✓ Events scheduled successfully!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }

            // Display upcoming events
            Console.WriteLine("Upcoming Events:");
            var upcomingEvents = scheduler.GetUpcomingEvents();
            foreach (var evt in upcomingEvents)
            {
                Console.WriteLine($"  • {evt.Name} at {evt.Location} on {evt.DateTime:MMM dd, yyyy h:mm tt}");
            }

            // Demo: Try to create a double-booking (this should fail)
            Console.WriteLine("\nAttempting to double-book (should be prevented):");
            try
            {
                scheduler.ScheduleEvent("Duplicate Event", "Another Room", new DateTime(2025, 11, 10, 10, 0, 0));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"✓ Double-booking prevented: {ex.Message}");
            }

            // Demo: Cancel an event
            Console.WriteLine("\nCancelling 'Team Meeting':");
            bool cancelled = scheduler.CancelEvent("Team Meeting");
            if (cancelled)
            {
                Console.WriteLine("✓ Event cancelled successfully");
            }

            // Save events to persistent storage
            scheduler.SaveEvents();
            Console.WriteLine("\n✓ Events saved to storage");
        }

        #endregion
    }

    #region Event and EventScheduler Classes

    /// <summary>
    /// Represents an event with a name, location, and date/time.
    /// </summary>
    public class Event
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime DateTime { get; set; }

        public Event() { }

        public Event(string name, string location, DateTime dateTime)
        {
            Name = name;
            Location = location;
            DateTime = dateTime;
        }
    }

    /// <summary>
    /// Manages scheduling, cancellation, and retrieval of events.
    /// Prevents double-booking and provides persistent storage.
    /// </summary>
    public class EventScheduler
    {
        private List<Event> events;
        private const string StorageFilePath = "events.json";

        public EventScheduler()
        {
            events = new List<Event>();
        }

        /// <summary>
        /// Schedules a new event. Throws an exception if double-booking is detected.
        /// </summary>
        public void ScheduleEvent(string name, string location, DateTime dateTime)
        {
            // Check for double-booking
            if (IsTimeSlotBooked(dateTime))
            {
                throw new InvalidOperationException(
                    $"Cannot schedule event. Time slot {dateTime:MMM dd, yyyy h:mm tt} is already booked.");
            }

            Event newEvent = new Event(name, location, dateTime);
            events.Add(newEvent);
        }

        /// <summary>
        /// Cancels an event by name. Returns true if successful, false if not found.
        /// </summary>
        public bool CancelEvent(string eventName)
        {
            Event eventToCancel = events.FirstOrDefault(e => 
                e.Name.Equals(eventName, StringComparison.OrdinalIgnoreCase));

            if (eventToCancel != null)
            {
                events.Remove(eventToCancel);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a list of all upcoming events sorted by date/time.
        /// </summary>
        public List<Event> GetUpcomingEvents()
        {
            DateTime now = DateTime.Now;
            return events
                .Where(e => e.DateTime >= now)
                .OrderBy(e => e.DateTime)
                .ToList();
        }

        /// <summary>
        /// Checks if a specific time slot is already booked.
        /// </summary>
        private bool IsTimeSlotBooked(DateTime dateTime)
        {
            return events.Any(e => e.DateTime == dateTime);
        }

        /// <summary>
        /// Saves all events to persistent storage (JSON file).
        /// </summary>
        public void SaveEvents()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(events, new JsonSerializerOptions 
                { 
                    WriteIndented = true 
                });
                File.WriteAllText(StorageFilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving events: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads events from persistent storage (JSON file).
        /// </summary>
        public void LoadEvents()
        {
            try
            {
                if (File.Exists(StorageFilePath))
                {
                    string jsonString = File.ReadAllText(StorageFilePath);
                    events = JsonSerializer.Deserialize<List<Event>>(jsonString) ?? new List<Event>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading events: {ex.Message}");
                events = new List<Event>();
            }
        }
    }

    #endregion
}
