using System.ComponentModel.DataAnnotations;

namespace EventSchedularProject.Models
{
    public class EventWithoutDB
    {
        [Required(ErrorMessage = "Please enter name of the event")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter location of the event")]
        public string? Location { get; set; }

        [DateGreaterThanToday]
        [PreventDuplicateBooking]
        public DateTime DateTime { get; set; }
    }
}
