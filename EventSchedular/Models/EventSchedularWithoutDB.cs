namespace EventSchedularProject.Models
{
    public class EventSchedularWithoutDB
    {
        private static List<Event> allEvents = new List<Event>();

        public static IEnumerable<Event> AllEvents
        {
            get { return allEvents; }
        }

        public static void ScheduleEvent(Event newEvent)
        {
            allEvents.Add(newEvent);
        }

        public static void CancelEvent(Event delEvent)
        {
            allEvents.Remove(delEvent);
        }
    }
}
