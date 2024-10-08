using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using OfficeTeam2_.Models; // Make sure the namespace matches your Event model

namespace OfficeTeam2_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Events.json");

        [HttpGet]
        public IActionResult GetEvents()
        {
            // Ensure the file exists
            if (!System.IO.File.Exists(jsonFilePath))
            {
                return NotFound("Events.json file not found.");
            }

            // Read the JSON file contents
            var jsonContent = System.IO.File.ReadAllText(jsonFilePath);
            Console.WriteLine(jsonContent); // Debugging: output to verify content

            // Deserialize the content into a list of Event objects
            var events = JsonSerializer.Deserialize<List<Event>>(jsonContent);

            return Ok(events);
        }
    }
}
