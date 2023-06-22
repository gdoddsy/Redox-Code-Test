using System;
using System.Collections.Generic;

namespace Redox_Code_Test
{
    public class Event
    {
        public Event(string name, string location, DateTime startTime)
        {
            Name = name;
            Location = location;
            StartTime = startTime;
        }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
    }

    public class EventScheduler
    {
        private List<Event> evtList = new List<Event>();

        public bool ScheduleEvent(Event evt)
        {
            bool isDoubleBooked = this.isDoubleBooked(evt);
            if (isDoubleBooked)
                return false;

            evtList.Add(evt);
            return true;
        }
        public bool CancelEvent(Event evt)
        {
            Event matchedEvt = evtList.FirstOrDefault(p => p.Name.Equals(evt.Name)
            && p.Location.Equals(evt.Location) && p.StartTime.Equals(evt.StartTime));

            if (matchedEvt == null)
                return false;

            evtList.Remove(matchedEvt);
            return true;
        }

        public List<Event> GetUpcomingEvents(int hours = 24)
        {
            DateTime now = DateTime.Now;
            DateTime till = now.AddHours(hours);
            List<Event> list = new List<Event>();
            foreach (var evt in evtList)
            {
                if (evt.StartTime <= till && evt.StartTime >= now)
                    list.Add(evt);
            }
            return list;
        }

        private bool isDoubleBooked(Event newEvt)
        {
            if (newEvt == null || newEvt.StartTime == DateTime.MinValue) return true;

            return evtList.Exists(p => p.StartTime == newEvt.StartTime);
        }

    }
}
