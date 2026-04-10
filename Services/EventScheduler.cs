using Redox_Code_Test.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Redox_Code_Test.Services
{
    /// <summary>
    /// Handles event scheduling, cancellation, retrieval, and persistence
    /// </summary>
    public class EventScheduler
    {
        // In-memory list of events
        private List<Event> _events = new();

        // File path for storing events (JSON persistence)
        private const string FilePath = "events.json";

        /// <summary>
        /// Constructor - loads events from file during initialization
        /// </summary>
        public EventScheduler()
        {
            LoadEvents();
        }

        /// <summary>
        /// Schedules a new event while preventing time conflicts
        /// </summary>
        /// <param name="newEvent">Event to be scheduled</param>
        /// <returns>True if scheduled successfully, false if conflict exists</returns>
        public bool ScheduleEvent(Event newEvent)
        {
            if (newEvent == null)
                throw new ArgumentNullException(nameof(newEvent));

            // Check for time conflict (same DateTime)
            bool hasConflict = _events.Any(e => e.DateTime == newEvent.DateTime);

            if (hasConflict)
                return false;

            _events.Add(newEvent);
            SaveEvents();
            return true;
        }

        /// <summary>
        /// Cancels an event by name (case-insensitive)
        /// </summary>
        /// <param name="name">Event name</param>
        /// <returns>True if removed, false if not found</returns>
        public bool CancelEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            // Case-insensitive search for better usability
            var ev = _events.FirstOrDefault(e =>
                e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (ev == null)
                return false;

            _events.Remove(ev);
            SaveEvents();
            return true;
        }

        /// <summary>
        /// Returns all upcoming events sorted by date
        /// </summary>
        public IReadOnlyList<Event> GetUpcomingEvents()
        {
            return _events
                .Where(e => e.DateTime > DateTime.Now)
                .OrderBy(e => e.DateTime)
                .ToList();
        }

        /// <summary>
        /// Saves events to JSON file (persistent storage)
        /// </summary>
        private void SaveEvents()
        {
            try
            {
                var json = JsonSerializer.Serialize(
                    _events,
                    new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                // In real apps, log instead of Console
                Console.WriteLine($"Error saving events: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads events from JSON file into memory
        /// </summary>
        private void LoadEvents()
        {
            try
            {
                if (!File.Exists(FilePath))
                    return;

                var json = File.ReadAllText(FilePath);

                // Safely deserialize or fallback to empty list
                _events = JsonSerializer.Deserialize<List<Event>>(json) ?? new List<Event>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading events: {ex.Message}");
                _events = new List<Event>();
            }
        }
    }
}