using Microsoft.AspNetCore.Mvc;
using OfficeTeam2_.Models;
using System.Collections.Generic;

namespace OfficeTeam2_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private static List<Event> events = new List<Event>
        {
            new Event 
            { 
                Id = 1, 
                Title = "Event 1", 
                Description = "Description of Event 1", 
                Date = DateTime.Now, 
                StartTime = new TimeSpan(14, 0, 0), // 2 PM
                EndTime = new TimeSpan(16, 0, 0), // 4 PM
                Location = "Location 1",
                Reviews = new List<string> { "Great event!" },
                Attendees = new List<string> { "Attendee 1", "Attendee 2" }
            },
            new Event 
            { 
                Id = 2, 
                Title = "Event 2", 
                Description = "Description of Event 2", 
                Date = DateTime.Now.AddDays(1), // Tomorrow
                StartTime = new TimeSpan(10, 0, 0), // 10 AM
                EndTime = new TimeSpan(12, 0, 0), // 12 PM
                Location = "Location 2",
                Reviews = new List<string> { "Had a great time!" },
                Attendees = new List<string> { "Attendee 3", "Attendee 4" }
            }
        };

        [HttpGet]
        public ActionResult<List<Event>> GetEvents()
        {
            return Ok(events);
        }
    }
}
