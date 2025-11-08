using System;

namespace Redox_Code_Test
{
    /// <summary>
    /// Represents an event with a name, location, and date/time
    /// This is a simple class that holds information about an event
    /// </summary>
    public class Event
    {
        // Properties are like variables that belong to a class
        // They allow us to store and access data about an event

        /// <summary>
        /// The name or title of the event
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The location where the event will take place
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The date and time when the event is scheduled
        /// DateTime is a built-in C# type that represents a specific moment in time
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}


