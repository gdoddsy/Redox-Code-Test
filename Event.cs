using System;

namespace RedoxCodeTest
{
    public class Event
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }

        public override string ToString()
            => $"{Name} @ {Location} on {DateTime:G}";
    }
}
