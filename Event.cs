using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redox_Code_Test
{
    public class Event
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime DateTime { get; set; }
        public Event(string name, string location, DateTime datetime)
        {
            this.Name = name;
            this.Location = location;
            this.DateTime = datetime;
        }
        public bool IsEqual(Event item)
        {
            return item.Location.Equals(this.Location) && item.DateTime.Equals(this.DateTime);
        }
    }
}
