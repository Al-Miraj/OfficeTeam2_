using Microsoft.AspNetCore.Mvc;


////////////////////////////////////////////////
// EventControllers.cs
////////////////////////////////////////////////
[Route("api/EventAttendance")]
public class EventControllers: Controller {
    public EventServices EventService;
    public EventControllers(EventServices eventService){
        EventService = eventService;
    }

    [HttpGet("h")]
    public async Task<IActionResult> Testt(){
        return Ok("worked.");
    }

    [HttpPost("Attend")]
    public async Task<IActionResult> AttendEvent([FromBody] EventAttendance eventAttendance) {
        if (EventService.CheckEventExistance(eventAttendance.Event_Id)){ // Q is this considered business logic? or is it fine here?
            JsonFileHandler.WriteToJsonFile("Data/EventAttendance.json", new List<EventAttendance>() {eventAttendance}); // Q should be awaitable?
            return Ok(eventAttendance.Event_Id);
        }
        else {
            return NotFound(eventAttendance.Event_Id); // Q good feedback response?
        } 
    }

    // basic authentication
    [HttpGet("FindEventAttendees/{event_id}")]
    public async Task<IActionResult> FindEventAttendees(Guid event_id){
        if (EventService.CheckEventExistance(event_id)){
            return Ok(EventService.ListEventAttendees(event_id));
        }
        else{
            return NotFound(event_id);
        }

    }

    [HttpGet("DeleteEventAttendance/{event_id}/{user_id}")]
    public async Task<IActionResult> DeleteEventAttendance(Guid event_id, Guid user_id){
        if (EventService.CheckEventExistance(event_id) && EventService.DeleteEventAttendance(event_id, user_id)){
            return Ok("deletion succesfull");
        }
        else{
            return Conflict();
        }

    }
}