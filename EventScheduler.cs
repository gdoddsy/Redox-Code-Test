using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redox_Code_Test
{
    public class EventScheduler 
    {
        private static EventScheduler _instance;
        private List<Event> events = new List<Event>();
        public static EventScheduler Instance => _instance ?? (_instance = new EventScheduler());
        public bool IsDuplicate(Event item)
        {
            return (this.events.Where(f => item.IsEqual(f)).Count() > 0);
        }

        public Event this[int index]
        {
            get { return this.events[index]; }
            set
            {
                if (IsDuplicate(value))
                {
                    //throw new ArgumentException("Duplicate item");
                    Console.WriteLine("Indexed update not possible, duplicate  item: {0}", value);
                }
                else
                {
                    this.events[index] = value;
                };
            }
        }

       
        public int IndexOf(Event item) { return this.events.IndexOf(item); }
        public void ScheduleEvent(int index, Event item)
        {
            if (IsDuplicate(item))
            {
                //throw new ArgumentException("Duplicate item");
                Console.WriteLine("Insert not possible, duplicate event: {0}", item);
            }
            else
            {
                this.events.Insert(index, item);
            };
        }
        public bool CancelEvent(Event item) { return this.events.Remove(item); }
        public void RemoveAt(int index) { this.events.RemoveAt(index); }
        public IEnumerator GetUpcomingEvents()
        {
            return this.events.GetEnumerator();
        }
    }

      
    
}
