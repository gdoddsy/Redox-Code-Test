using EventSchedularProject.Models;
using System.Collections.Generic;

namespace EventSchedularProject.Repository
{
    public interface IEvent
    {
        Task<int> ScheduleEvent(Event e);
        Task<int> CancelEvent(int id);

        Task<int> UpdateEvent(Event e);

        Event GetEventById(int id);

        Task<IEnumerable<Event>> GetUpcomingEvents();

        Task<IEnumerable<Event>> GetAllEvents();

        Event GetDuplicateEvent(DateTime dt);
        
    }
    }
