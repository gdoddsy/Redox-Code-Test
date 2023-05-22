using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventSchedularProject.Models;
using EventSchedularProject.Data;
using EventSchedularProject.Repository;
using Microsoft.AspNetCore.Components;

namespace EventSchedularProject.Controllers
{
    public class EventController : Controller
    {
        private readonly IEvent ev;

        public EventController(IEvent ev)
        {
            this.ev = ev;
        }

        
        public ActionResult Index()
        {
            // getting all the events
            try
            {
                var getEvents = ev.GetAllEvents().Result;               
                return View(getEvents);
            }
            catch (Exception ex)
            {
               ModelState.AddModelError("Error", ex.Message ); 
            }
            return View();
        }

        public ActionResult GetUpcomingEvents()
        {
            //getting only future events
            try
            {
                var getEvents = ev.GetUpcomingEvents().Result;
                return View("UpComing", getEvents);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message); 
            }
            return View("UpComing");
        }

        // GET: EventController/Create
        public IActionResult ScheduleEvent()
        {
            return View();
        }

        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ScheduleEvent(Event newEvent)
        {
            //creating a new event after validating non existence of duplicate event with same date time 
            if (ModelState.IsValid)
            {
                 try
                 {
                    //check duplicate event with same datetime
                    Event dupevent = ev.GetDuplicateEvent(newEvent.DateTime);
                    if (dupevent != null)
                    {
                        ModelState.AddModelError("Duplicate", "Another Event is already scheduled for this date time");
                        return View();
                    }
                    else
                    {

                        ev.ScheduleEvent(newEvent);
                        ModelState.Clear();
                        return View("Thanks", newEvent);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message); 
                }
            }
            else
             
                {
                    return View();
                }
            return View();

        }

        // GET: EventController/Edit/5
        public IActionResult Update(int id)
        {
            try
            {
                Event updEvent = ev.GetEventById(id);

                return View("Update", updEvent);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message); 
            }
            return View();
        }

        // POST: EventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Event eventupd)
        {
            //update event 
            if (ModelState.IsValid)
            {
                try
                {
                    //check duplicate event with same datetime
                    Event  resevent = ev.GetDuplicateEvent(eventupd.DateTime);
                    if (resevent != null && resevent.Id != eventupd.Id)
                    {
                        ModelState.AddModelError("Duplicate", "Another Event is already scheduled for this date time");
                        return View();
                    }
                    else
                    {
                        
                        Event newevent = new Event()
                        {
                            Id = eventupd.Id,
                            Name = eventupd.Name,
                            Location = eventupd.Location,
                            DateTime = eventupd.DateTime
                        };
                        var result = ev.UpdateEvent(newevent);
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }                    
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message); 
                }
            }
           else
            {
                return View();
            }
            return View();
        }


        public IActionResult CancelEvent(int id)
        {
            try
            {
                Event eventcal = ev.GetEventById(id);

                return View("Cancel", eventcal);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message); 
            }
            return View();
        }

        // POST: EventController/Delete/5

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CancelEvent(Event evcancel)
        {
            //delete event
            try { 
            var result = ev.CancelEvent(evcancel.Id);

            return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message); 
            }
            return View();
        }

        
      
    }
}
