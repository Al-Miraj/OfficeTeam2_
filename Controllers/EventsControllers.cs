using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using OfficeTeam2_.Models; // Ensure you have the correct namespace

namespace OfficeTeam2_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        // GET /events - Get all events
        [HttpGet]
        public IActionResult GetEvents()
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Events.json");

            if (System.IO.File.Exists(jsonFilePath))
            {
                var jsonData = System.IO.File.ReadAllText(jsonFilePath);
                var events = JsonSerializer.Deserialize<List<Event>>(jsonData);
                return Ok(events);
            }

            return NotFound("Events file not found.");
        }

        // GET /events/{id} - Get event by Id
        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Events.json");

            if (System.IO.File.Exists(jsonFilePath))
            {
                var jsonData = System.IO.File.ReadAllText(jsonFilePath);
                var events = JsonSerializer.Deserialize<List<Event>>(jsonData);
                var eventItem = events.FirstOrDefault(e => e.Id == id);

                if (eventItem != null)
                {
                    return Ok(eventItem);
                }

                return NotFound($"Event with Id {id} not found.");
            }

            return NotFound("Events file not found.");
        }

        // POST /events - Add a new event
        [HttpPost]
        public IActionResult AddEvent([FromBody] Event newEvent)
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Events.json");

            if (System.IO.File.Exists(jsonFilePath))
            {
                var jsonData = System.IO.File.ReadAllText(jsonFilePath);
                var events = JsonSerializer.Deserialize<List<Event>>(jsonData);

                newEvent.Id = events.Max(e => e.Id) + 1;  // Increment Id for the new event
                events.Add(newEvent);

                System.IO.File.WriteAllText(jsonFilePath, JsonSerializer.Serialize(events, new JsonSerializerOptions { WriteIndented = true }));

                return Ok(newEvent);
            }

            return NotFound("Events file not found.");
        }

        // PUT /events/{id} - Update an event
        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] Event updatedEvent)
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Events.json");

            if (System.IO.File.Exists(jsonFilePath))
            {
                var jsonData = System.IO.File.ReadAllText(jsonFilePath);
                var events = JsonSerializer.Deserialize<List<Event>>(jsonData);

                var eventItem = events.FirstOrDefault(e => e.Id == id);
                if (eventItem != null)
                {
                    eventItem.Name = updatedEvent.Name;
                    eventItem.Date = updatedEvent.Date;
                    eventItem.Location = updatedEvent.Location;
                    eventItem.Attendees = updatedEvent.Attendees;

                    System.IO.File.WriteAllText(jsonFilePath, JsonSerializer.Serialize(events, new JsonSerializerOptions { WriteIndented = true }));

                    return Ok(eventItem);
                }

                return NotFound($"Event with Id {id} not found.");
            }

            return NotFound("Events file not found.");
        }

        // DELETE /events/{id} - Delete an event
        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Events.json");

            if (System.IO.File.Exists(jsonFilePath))
            {
                var jsonData = System.IO.File.ReadAllText(jsonFilePath);
                var events = JsonSerializer.Deserialize<List<Event>>(jsonData);

                var eventItem = events.FirstOrDefault(e => e.Id == id);
                if (eventItem != null)
                {
                    events.Remove(eventItem);

                    System.IO.File.WriteAllText(jsonFilePath, JsonSerializer.Serialize(events, new JsonSerializerOptions { WriteIndented = true }));

                    return Ok($"Event with Id {id} deleted.");
                }

                return NotFound($"Event with Id {id} not found.");
            }

            return NotFound("Events file not found.");
        }
    }
}
