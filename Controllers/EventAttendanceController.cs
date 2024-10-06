using Microsoft.AspNetCore.Mvc;


////////////////////////////////////////////////
// EventControllers.cs
////////////////////////////////////////////////
[Route("api/EventAttendance")]
public class EventControllers: Controller {
    public IEventAttendanceService EventService;
    public EventControllers(IEventAttendanceService eventService){
        EventService = eventService;
    }

    [HttpGet("h")]
    public IActionResult Testt(){
        return Ok("worked.");
    }

    [HttpPost("Attend")]
    public async Task<IActionResult> AttendEvent([FromBody] EventAttendance eventAttendance) {
        bool result = await EventService.AttendEventAsync(eventAttendance);
        
        if (result) {
            return Ok($"Attendance to event with ID {eventAttendance.Event_Id} succeeded.");
        } else {
            return BadRequest($"Attendance to event with ID{eventAttendance.Event_Id} failed."); // Q good feedback response?
        } 
    }

    // basic authentication
    [HttpGet("FindEventAttendees/{event_id}")]
    public async Task<IActionResult> FindEventAttendees(Guid event_id){
        if (EventService.CheckEventExistance(event_id) == false){
            return NotFound($"Event with ID {event_id} not found.");
        }

        List<Guid> result = await EventService.ListEventAttendeesAsync(event_id);
        return Ok(result);
    }

    [HttpGet("DeleteEventAttendance/{event_id}/{user_id}")]
    public async Task<IActionResult> DeleteEventAttendance(Guid event_id, Guid user_id){
        bool result = await EventService.DeleteEventAttendanceAsync(event_id, user_id);
        if (result){
            return Ok($"Deletion of event attendance to event with ID {event_id} and user ID {user_id} succesfull");
        }
        else{
            return NotFound($"Event attendance with event ID {event_id} and user ID {user_id} not found.");
        }

    }
}