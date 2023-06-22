using System;
using System.Collections.Generic;
using System.Linq;

class EventScheduler
{
    private List<Event> events = new List<Event>();

    public void ScheduleEvent(Event newEvent)
    {
        if (IsTimeBooked(newEvent))
        {
            Console.WriteLine("Error! Event scheduling failed. Time has been booked for another event.");
            return;
        }
        else if (IsTimeInThePast(newEvent))
        {
        	Console.WriteLine("Error! Event scheduling failed. Time requested is in the past.");
            return;
        }        

        events.Add(newEvent);
        Console.WriteLine(newEvent.Name + " at " + newEvent.Location + " scheduled at " + newEvent.DateTime + " BOOKED successfully.");
    }

    public void CancelEvent(Event eventToCancel)
    {
        events.Remove(eventToCancel);
        Console.WriteLine(eventToCancel.Name + " at " + eventToCancel.Location + " originally scheduled at " + eventToCancel.DateTime + " CANCELLED successfully.");
    }

    public void GetUpcomingEvents()
    {
    	Console.WriteLine("Upcoming Events:");
        List<Event> upcomingEvents = events
        .Where(e => e.DateTime > System.DateTime.Now) // only include events in the future
        .OrderBy(e => e.DateTime).ToList();
        
        upcomingEvents.ForEach(p => Console.WriteLine($"Name: {p.Name}, Location: {p.Location}, DateTime: {p.DateTime}"));
    }

    private bool IsTimeBooked(Event newEvent)
    {
        return events.Any(e => e.DateTime == newEvent.DateTime); //reject scheduling if there is any event that has already been scheduled at the same time
    }
    
    private bool IsTimeInThePast(Event newEvent)
    {
    	return newEvent.DateTime < System.DateTime.Now; // reject scheduling if the requested time is in the past
    }
    
}