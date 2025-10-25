using System;
using System.Collections.Generic;
using System.Linq;

namespace Redox_Code_Test
{
    public class Program
    {   
        public static void Main(string[] args)
        {
			Exercise exercise1 = new Exercise1();
			Exercise exercise2 = new Exercise2();
			exercise1.Run();
			exercise2.Run();
        }
    }
	
	public interface Exercise
	{
		void Run();
	}
		
	public class Exercise1 : Exercise
	{
		public void Run()
		{
			// 1: Create a list of integers from 1 to 100.
			int[] numbers = Enumerable.Range(1,100).ToArray();
			
			// 2: Use LINQ to find all even numbers in the list and print them.
			var evens = from number in numbers
						where (number % 2) == 0
						select number;
			foreach (int number in evens)
			{
				Console.WriteLine(number);
			}
			
			// 3: Use a loop to find all of the numbers in the original list 
			//    that are divisible by 3 or 5, but not 3 and 5.
			foreach (int number in numbers)
			{
				bool three = number % 3 == 0;
				bool five = number % 5 == 0;
				if (three && five) continue;
				else if (three || five) Console.WriteLine(number);
			}
		}
	}
	
	public class Exercise2 : Exercise
	{
		public void Run()
		{
			EventScheduler eventScheduler = new EventScheduler();
			Event e1 = new Event("Event 1", "Location 1", new DateTime(2025, 1, 1, 12, 0, 0));
			Event e2 = new Event("Event 2", "Location 2", new DateTime(2025, 1, 1, 12, 0, 1));
			Event e3 = new Event("Event 3", "Location 2", new DateTime(2025, 1, 1, 12, 0, 1));
			eventScheduler.ScheduleEvent(e1);
			eventScheduler.ScheduleEvent(e2);
			eventScheduler.ScheduleEvent(e3);
			eventScheduler.CancelEvent(e2);
			eventScheduler.ScheduleEvent(e3);
			eventScheduler.GetUpcomingEvents();
		}
	}
	
	public class Event
	{
		private string name;
		private string location;
		private DateTime dateTime;
		
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		public string Location
		{
			get { return location; }
			set { location = value; }
		}
		public DateTime DateTime
		{
			get { return dateTime; }
			set { dateTime = value; }
		}
		
		public Event(string name, string location, DateTime dateTime)
		{
			Name = name;
			Location = location;
			DateTime = dateTime;
		}
	}
	
	public class EventScheduler
	{
		private List<Event> events = new List<Event>();
		
		public void ScheduleEvent(Event e)
		{
			foreach (Event scheduledEvent in events)
			{
				if (e.DateTime == scheduledEvent.DateTime)
				{
					Console.WriteLine("Error - you cannot schedule two events for the same time");
					return;
				}
			}
			events.Add(e);
			Console.WriteLine($"Event {e.Name} Scheduled");
		}
		
		public void CancelEvent(Event e)
		{
			events.Remove(e);
			Console.WriteLine($"Event {e.Name} Cancelled");
		}
		
		public void GetUpcomingEvents()
		{
			foreach (Event e in events)
			{
				Console.WriteLine($"Event Details - Name: {e.Name}, Location: {e.Location}, Time: {e.DateTime}");
			}
		}
	}
}