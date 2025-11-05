using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace RedoxCodeTest
{
    public class EventScheduler
    {
        private readonly List<Event> _events = new();
        private readonly string _storagePath;

        private static readonly JsonSerializerOptions JsonOpts = new()
        {
            WriteIndented = true
        };

        public EventScheduler(string storagePath = "events.json")
        {
            _storagePath = storagePath;
            Load();
        }

        // Schedule an event; prevent exact same DateTime (double-booking)
        public void ScheduleEvent(Event e)
        {
            if (_events.Any(x => x.DateTime == e.DateTime))
                throw new InvalidOperationException($"An event already exists at {e.DateTime:G}.");

            _events.Add(e);
            Save();
        }

        // Cancel by (Name, DateTime). Return true if removed.
        public bool CancelEvent(string name, DateTime dateTime)
        {
            var removed = _events.RemoveAll(e => e.Name == name && e.DateTime == dateTime) > 0;
            if (removed) Save();
            return removed;
        }

        // Upcoming events (>= now), ordered by time
        public IReadOnlyList<Event> GetUpcomingEvents()
            => _events.Where(e => e.DateTime >= DateTime.Now)
                      .OrderBy(e => e.DateTime)
                      .ToList();

        // Helper used by the demo to reset to a clean state
        public void ReplaceAll(IEnumerable<Event> events)
        {
            _events.Clear();
            _events.AddRange(events);
            Save();
        }

        private void Load()
        {
            if (!File.Exists(_storagePath)) return;
            var json = File.ReadAllText(_storagePath);
            var loaded = JsonSerializer.Deserialize<List<Event>>(json) ?? new List<Event>();
            _events.Clear();
            _events.AddRange(loaded);
        }

        private void Save()
        {
            var json = JsonSerializer.Serialize(_events, JsonOpts);
            File.WriteAllText(_storagePath, json);
        }
    }
}
