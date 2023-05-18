using System;
using System.Text.Json;

namespace Redox_Code_Test
{
    /// <summary>
    /// Event Class
    /// </summary>
    class Event
    {
        /// <summary>
        /// The name of the event
        /// </summary>
        private string Name;
        /// <summary>
        /// The location of the event
        /// </summary>
        private string Location;
        /// <summary>
        /// The time of the event
        /// </summary>
        private DateTimeOffset DateTime;

        /// <summary>
        /// Creates a new event instance.
        /// </summary>
        /// <param name="name">The name of the event. Defaults to an empty name.</param>
        /// <param name="location">The location of the event</param>
        /// <param name="dateTime"></param>
        public Event(string name = "", string location = "", DateTimeOffset? dateTime)
        {
            this.Name = name;
            this.Location = location;
            if (dateTime.HasValue)
                this.DateTime = (DateTimeOffset)dateTime;
            else
                this.DateTime = DateTimeOffset.Now;
        }

        /// <summary>
        /// Retrieve the name of the event
        /// </summary>
        /// <returns>The events name</returns>
        public string nameGet() => this.Name;

        /// <summary>
        /// Retrieve the location of the event
        /// </summary>
        /// <returns>The events location</returns>
        public string locationGet() => this.Location;

        /// <summary>
        /// Retrieve the Date and Time with offset for the event
        /// </summary>
        /// <returns>The date and time of the event</returns>
        public DateTimeOffset dateTimeGet() => this.DateTime;

        /// <summary>
        /// Updating the name of the event
        /// </summary>
        /// <param name="name">The new name for the event</param>
        public void nameSet(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Updating the location of the event
        /// </summary>
        /// <param name="location">The new location of the event</param>
        public void locationSet(string location)
        {
            this.Location = location;
        }

        /// <summary>
        /// Update the date and time of the event
        /// </summary>
        /// <param name="dateTime">The new date and time for the event</param>
        public void dateTimeSet(DateTimeOffset dateTime)
        {
            this.DateTime = dateTime;
        }

        /// <summary>
        /// Return the event as a object (Similar to that of a JSON item)
        /// </summary>
        /// <returns>The events details in object form</returns>
        public object getEvent()
        {
            return new 
            {
               name = this.Name,
               location = this.Location,
               dateTime = this.DateTime
            };
        }
    }
}
