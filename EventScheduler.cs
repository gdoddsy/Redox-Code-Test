using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace Redox_Code_Test
{
    public class EventScheduler
    {
        //List to store events
        private List<Event> events;
        private const string StorageFile = "events.json";

        public EventScheduler()
        {
            events = LoadEventsFromStorage();
        }

        public bool ScheduleEvent(Event newEvent)
        {
            //Check for double booking
            if (events.Any(e => e.DateTime == newEvent.DateTime))
            {
                Console.WriteLine("Error: An event is already scheduled at this time.");
                return false;
            }

            //Add new event and save to storage
            events.Add(newEvent);
            SaveEventsToStorage();
            Console.WriteLine("Event scheduled successfully.");
            return true;
        }

        public bool CancelEvent(string eventName)
        {
            //Find and remove event by name
            var eventToCancel = events.FirstOrDefault(e => e.Name == eventName);
            if (eventToCancel != null)
            {
                //Remove and save changes
                events.Remove(eventToCancel);
                SaveEventsToStorage();
                Console.WriteLine("Event canceled successfully.");
                return true;
            }

            Console.WriteLine("Error: Event not found.");
            return false;
        }

        public List<Event> GetAllEvents()
        {
            //Return all events
            return events; 
        }

        private void SaveEventsToStorage()
        {
            //Serialize events to JSON and save to file
            var json = JsonSerializer.Serialize(events);
            File.WriteAllText(StorageFile, json);
        }

        private List<Event> LoadEventsFromStorage()
        {
            //Load events from JSON file if it exists
            if (File.Exists(StorageFile))
            {
                var json = File.ReadAllText(StorageFile);
                var loadedEvents = JsonSerializer.Deserialize<List<Event>>(json);
                return loadedEvents ?? new List<Event>();
            }

            return new List<Event>();
        }
    }
}