using System;
using System.Collections.Generic;
using System.IO;
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
			int[] numbers = Enumerable.Range(1, 100).ToArray();
			
			var evens = from number in numbers
						where (number % 2) == 0
						select number;
			foreach (int number in evens)
			{
				Console.WriteLine(number);
			}
			
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
			// Some example use cases I made to demonstrate the solution working
			EventScheduler eventScheduler = new EventScheduler();
			Event e1 = new Event("Event 1", "Location 1", new DateTime(2025, 1, 1, 12, 0, 0));
			Event e2 = new Event("Event 2", "Location 2", new DateTime(2025, 1, 1, 12, 0, 1));
			Event e3 = new Event("Event 3", "Location 3", new DateTime(2025, 1, 1, 12, 0, 1));
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

		public Event() {}
		
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

		public EventScheduler()
        {
			string eventStorage = "storage.txt";
			try
			{
				string[] storedEvents = File.ReadAllLines(eventStorage);
				foreach (string e in storedEvents)
				{
					Event newEvent = new Event();

					var data = e.Split(',');
					newEvent.Name = data[0];
					newEvent.Location = data[1];
					newEvent.DateTime = DateTime.Parse(data[2]);

					events.Add(newEvent);
				}
			}
			catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception}");
            }
		}
		
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
			UpdateStorage();
			Console.WriteLine($"Event {e.Name} Scheduled");
		}
		
		public void CancelEvent(Event e)
		{
			events.Remove(e);
			UpdateStorage();
			Console.WriteLine($"Event {e.Name} Cancelled");
		}

		public void GetUpcomingEvents()
		{
			foreach (Event e in events)
			{
				Console.WriteLine($"Event Details - Name: {e.Name}, Location: {e.Location}, Time: {e.DateTime}");
			}
		}
		
		private void UpdateStorage()
        {
			try
			{
				string eventStorage = "storage.txt";
				File.WriteAllText(eventStorage, string.Empty);
				
				foreach (Event e in events)
				{
					string name = e.Name;
					string location = e.Location;
					string dateTime = e.DateTime.ToString("yyyy-MM-dd HH:mm:ss");

					List<string> lines = new List<string>();
					string line = name + "," + location + "," + dateTime;
					lines.Add(line);
					File.AppendAllLines(eventStorage, lines);
				}
			}
			catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception}");
            }
		}
	}
}