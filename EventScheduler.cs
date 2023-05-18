using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redox_Code_Test
{
    /// <summary>
    /// Event Scheduler Class
    /// </summary>
    class EventScheduler
    {
        /// <summary>
        /// List of the events within the schedule
        /// </summary>
        private List<Event> events;

        /// <summary>
        /// Creates a new event schedule
        /// </summary>
        public EventScheduler() 
        { 
            this.events = new List<Event>(); 
        }

        /// <summary>
        /// Adds a new event to the schedule.
        /// If the new event clashes times with an existing event, the event will not be added.
        /// </summary>
        /// <param name="newEvent">The event to be added.</param>
        /// <returns>True if event has been added. False otherwise.</returns>
        public bool ScheduleEvent(Event newEvent)
        {
            bool clash = false;

            // Check to see if there is an existing event with the same start time. If so return false.
            this.events.ToList().ForEach((Event e) => {
                if (!clash && e.dateTimeGet().CompareTo(newEvent.dateTimeGet()) == 0)
                    clash = true;
            });

            if (clash)
                return false;

            // Add the event to the list of events.
            this.events.Add(newEvent);

            // Check the list of events to make sure the new event is contained within it.
            // Return false if it is not found. True otherwise.
            bool located = false;
            this.events.ToList().ForEach((Event e) => {
                if (!located && e.getEvent() == newEvent.getEvent())
                    located = true;
            });

            if (located)
                return true;
            return false;
        }

        /// <summary>
        /// Remove a desired event from the schedule
        /// </summary>
        /// <param name="removeEvent">The event to be removed</param>
        /// <returns>True if the event has been removed. False otherwise.</returns>
        public bool CancelEvent(Event removeEvent)
        {
            Event? located = null;

            // Locate the event
            this.events.ToList().ForEach((Event e) => {
                if (located == null && e.getEvent() == removeEvent.getEvent())
                    located = e;
            });

            // If located remove the event. Otherwise return false
            if (located != null)
                this.events.Remove(removeEvent);
            else
                return false;

            // Check to see if the event has been removed. If so return true. False otherwise.
            located = null;

            this.events.ToList().ForEach((Event e) => {
                if (located == null && e.getEvent() == removeEvent.getEvent())
                {
                    located = e;
                }
            });

            if (located == null)
                return true;
            return false;
        }

        /// <summary>
        /// Retrieve the upcoming events from within the schedule.
        /// </summary>
        /// <param name="fromDate">The start date time offset for the events to be retrieved. If no value is provided, the current date time offset is used.</param>
        /// <returns>A list of the upcoming events.</returns>
        public List<Event> GetUpcomingEvents(DateTimeOffset? fromDate)
        {
            if (!fromDate.HasValue)
                fromDate = DateTimeOffset.Now;
            List<Event> upcoming = new List<Event>();
            
            foreach (Event e in this.events) {
                if (e.dateTimeGet().CompareTo((DateTimeOffset)fromDate) >= 0)
                    upcoming.Add(e);
            }

            return upcoming;
        }
    }
}
