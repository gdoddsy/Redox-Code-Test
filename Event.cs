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

    public bool Equals(Event other)
    {
        if (other == null) return false;
        return this.Location == other.Location && this.StartTime == other.StartTime;
    }
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
        Event matchedEvt = evtList.FirstOrDefault(p => p.Equals(evt));

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

    private bool isDoubleBooked(Event newEvent)
    {
        if (newEvent == null || newEvent.StartTime == null) return true;

        return evtList.Exists(p => p.Equals(newEvent));
    }

}
}
