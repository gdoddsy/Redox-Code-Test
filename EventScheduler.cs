using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Redox_Code_Test
{
    public class EventScheduler
    {
        // Private field to store the list of events
        // 'private' means this can only be accessed within this class
        // This is good practice - it protects our data from being modified incorrectly
        private List<Event> events;

        // File path where we'll save events to disk
        // This allows events to persist even after the program closes
        private const string StorageFilePath = "events.json";

        public EventScheduler()
        {
            // Initialize the list as an empty list
            events = new List<Event>();

            // Try to load events from file if it exists
            // This allows events to persist between program runs
            LoadEventsFromFile();
        }

        /// <summary>
        /// Schedules a new event
        /// Returns true if successful, false if the time slot is already booked
        /// </summary>
        /// <param name="newEvent">The event to schedule</param>
        /// <returns>True if event was scheduled, false if time slot is already taken</returns>
        public bool ScheduleEvent(Event newEvent)
        {
            // Check for double-booking: see if any existing event has the same date/time
            // Any() is a LINQ method that checks if any item in the list matches a condition
            bool timeSlotTaken = events.Any(e => e.DateTime == newEvent.DateTime);

            if (timeSlotTaken)
            {
                // Time slot is already booked, so we can't schedule this event
                return false;
            }

            // Time slot is available, so add the event to our list
            events.Add(newEvent);

            // Save the updated list to file so it persists
            SaveEventsToFile();

            // Return true to indicate success
            return true;
        }

        /// <summary>
        /// Cancels an event by removing it from the list
        /// </summary>
        /// <param name="eventName">The name of the event to cancel</param>
        public void CancelEvent(string eventName)
        {
            // Find the event with the matching name
            // FirstOrDefault() finds the first event that matches, or returns null if none found
            Event eventToCancel = events.FirstOrDefault(e => e.Name == eventName);

            if (eventToCancel != null)
            {
                // Remove the event from the list
                events.Remove(eventToCancel);

                // Save the updated list to file
                SaveEventsToFile();
            }
        }

        /// <summary>
        /// Gets all upcoming events (events that haven't happened yet)
        /// Events are sorted by date/time, with the soonest events first
        /// </summary>
        /// <returns>A list of upcoming events, sorted by date/time</returns>
        public List<Event> GetUpcomingEvents()
        {
            // Get the current date and time
            DateTime now = DateTime.Now;

            // Filter events to only include those in the future
            // Where() filters the list, keeping only events where DateTime > now
            // OrderBy() sorts the results by DateTime (earliest first)
            // ToList() converts the result back to a List
            return events
                .Where(e => e.DateTime > now)
                .OrderBy(e => e.DateTime)
                .ToList();
        }

        /// <summary>
        /// Saves the events list to a JSON file on disk
        /// This allows events to persist even after the program closes
        /// </summary>
        private void SaveEventsToFile()
        {
            try
            {
                // Convert the list of events to JSON format
                // JsonSerializer.Serialize() converts objects to a JSON string
                string json = JsonSerializer.Serialize(events, new JsonSerializerOptions
                {
                    WriteIndented = true // Makes the JSON file readable with proper indentation
                });

                // Write the JSON string to a file
                // File.WriteAllText() creates or overwrites the file with the JSON data
                File.WriteAllText(StorageFilePath, json);
            }
            catch (Exception ex)
            {
                // If something goes wrong (e.g., no write permissions), print an error
                // In a real application, you might want to log this error properly
                Console.WriteLine($"Error saving events to file: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads events from a JSON file on disk
        /// This is called when the EventScheduler is created
        /// </summary>
        private void LoadEventsFromFile()
        {
            try
            {
                // Check if the file exists
                if (File.Exists(StorageFilePath))
                {
                    // Read the JSON string from the file
                    string json = File.ReadAllText(StorageFilePath);

                    // Convert the JSON string back to a list of Event objects
                    // JsonSerializer.Deserialize() converts JSON back to objects
                    // The angle brackets <List<Event>> tell C# what type to convert to
                    events = JsonSerializer.Deserialize<List<Event>>(json);

                    // If deserialization returned null (e.g., file was empty), create a new list
                    if (events == null)
                    {
                        events = new List<Event>();
                    }
                }
            }
            catch (Exception ex)
            {
                // If something goes wrong (e.g., file is corrupted), start with an empty list
                // In a real application, you might want to log this error properly
                Console.WriteLine($"Error loading events from file: {ex.Message}");
                events = new List<Event>();
            }
        }
    }
}


