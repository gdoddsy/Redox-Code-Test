using EventSchedularProject.Models;
using Microsoft.EntityFrameworkCore;
using EventSchedularProject.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EventSchedularProject.Repository
{
    public class EventSchedular : IEvent

    {
        private readonly EventSchedularContext _context;
       

        public EventSchedular(EventSchedularContext context)
        {
            _context = context;
        }
        

        public async Task<int> ScheduleEvent(Event newEvent)
        {
            try
            {// add new event 
                if (newEvent != null)
                {
                    await _context.Event.AddAsync(newEvent);
                    await _context.SaveChangesAsync();
                    return newEvent.Id;

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return 0;
        }

        public async Task<int> CancelEvent(int id)
        {
            int Getresult = 0;

            try
            {// delete event
                if (id != null)
                {
                    // var rec_id = await _context.Event.FirstOrDefaultAsync(e => e.Id == id);
                    //if (rec_id != null)
                    //{
                    //   _context.Remove(_context.Event.Single(e => e.Id == id));
                    // Getresult = await _context.SaveChangesAsync();
                    //  _context.Event.Remove(rec_id);


                    Event eventtocancel = new Event() { Id = id };
                    _context.Entry(eventtocancel).State = EntityState.Deleted;
                    await _context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Getresult;
        }
        public async Task<int> UpdateEvent(Event updEvent)
        {
            try
            {//update the event
                if (updEvent != null)
                {
                    //_context.Entry(updEvent).State = EntityState.Detached;
                    _context.Event.Update(updEvent);
                   // _context.Entry(updEvent).State = EntityState.Detached;
                    await _context.SaveChangesAsync();
                    return updEvent.Id;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return 0;
        }

        public async Task<IEnumerable<Event>> GetUpcomingEvents()
        {
            try
            {// get all future events
                if (_context != null)
                {
                    return await _context.Event.Where(e => e.DateTime >= DateTime.Now.Date).ToListAsync();
                }
            }
            catch (Exception)
            { throw; }

            return null;
        }

        public Event GetDuplicateEvent(DateTime dtvalue)
        {
            try
            {// check if any event exists with same datetime
                if (_context != null)
                {
                    return _context.Event.FirstOrDefault(e => e.DateTime == dtvalue);
                   
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

      


        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            try
            {//get all events
                if (_context != null )
                {
                    return await _context.Event.ToListAsync();
                }
               
            }
            catch(Exception ex)
            {
                throw;
            }
            return null;
        }

        
        public Event GetEventById(int id)
        {
            try
            {// get event by Id

                if (_context != null)
                {
                    return _context.Event.FirstOrDefault(e => e.Id == id);

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
    }
}
