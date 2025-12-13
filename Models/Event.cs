using System;

namespace Redox_Code_Test.Models
{
    public class Event
    {
        public string Name {get; set;}
        //If we wanted a startDate and EndDate for the event - we could add StartDate and EndDate properties instead
        public DateTime DateTime {get; set;}
        //we assume location is a string type (i.e Room4, Room5) and not a coordinate
        public string Location {get; set;}
    }
}