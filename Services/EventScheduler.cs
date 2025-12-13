using Redox_Code_Test.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//use Json for storage
using System.Text.Json;

namespace Redox_Code_Test.Services
{
    //ideally restrict to a singleton later to ensure that multiple instances are not created
    public class EventScheduler
    {
        private List<Event> _events = new List<Event>();
        //Ensure we use a hashset for O(1) time complexity instead of O(n) for lookup
        //the disadvantage is we have O(2n) instead of O(n) space complexity (store same list twice)
        private HashSet<DateTime> _bookedTimes = new HashSet<DateTime>();
        private string _filePath = "persistantEvents.json"; //persistent storage location
        public EventScheduler(){
            //load from file constructor
            LoadPersistant();
        }
        public void SavePersistant(){
            var json = JsonSerializer.Serialize(_events, new JsonSerializerOptions {WriteIndented = true});
            File.WriteAllText(_filePath, json);
        }
        public void LoadPersistant()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                //deseriablize json into a event list
                var result = JsonSerializer.Deserialize<List<Event>>(json);
                //use result, or create empty event list if null
                _events = result ?? new List<Event>();
                // Rebuild HashSet from event datetimes
                _bookedTimes = _events.Select(e => e.DateTime).ToHashSet(); 
            }
        }



        public void ScheduleEvent(Event e)

        {
            //if event had starttime and endtime, we could check that e.Start < existing.end && e.end > existing.start
            //O(1) lookup with hashset instead of O(n) with a List.Any()
           if (_bookedTimes.Contains(e.DateTime))
            {
                throw new Exception("Time already booked");
            }
            _bookedTimes.Add(e.DateTime);
            _events.Add(e);
            SavePersistant();
        }
        public void CancelEvent(string name)
        {
            //need to check both datastructures if using hashset, instead of simply _events.RemoveAll()
            var toRemove = _events.FirstOrDefault(e => e.Name == name);
            if (toRemove != null)
            {
                //remove from bookedtimes hashset
                _bookedTimes.Remove(toRemove.DateTime);
                //remove from events 
                _events.Remove(toRemove);
            }
            SavePersistant();
           

        }
        public List<Event> GetUpcomingEvents()
        {
            return _events.Where(e => e.DateTime > DateTime.Now).ToList();
        }
    }
}