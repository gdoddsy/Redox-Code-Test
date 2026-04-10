using System;

namespace Redox_Code_Test.Models
{
    /// <summary>
    /// Represents a scheduled event with basic details.
    /// This model is used for storing and retrieving event information.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Gets or sets the name/title of the event.
        /// Example: "Team Meeting", "Doctor Appointment"
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the location where the event will take place.
        /// Example: "Sydney Office", "Zoom", "Home"
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the event is scheduled.
        /// This value is used for sorting, filtering, and conflict detection.
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}