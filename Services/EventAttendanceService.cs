////////////////////////////////////////////////
// EventAttendanceService.cs
////////////////////////////////////////////////


public class EventAttendanceService : IEventAttendanceService{
    // write attendance to attendance.json
    // check availabilty of attendance
    public bool CheckEventExistance(Guid event_Id){
        Event? event_ = this._getEvent(event_Id);
        return event_ is not null; // true if event exists, false if not
    }

    private bool _checkAttendanceOnTime(Guid event_Id){ // Q null check or not???
        Event? event_ = _getEvent(event_Id);
        if(event_ == null) return false; // If the event doesn't exist, attendance is invalid.

        TimeOnly now = TimeOnly.FromDateTime(DateTime.Now);
        return event_.Start_Time > now && event_.Date >= DateTime.Now; // true if its before the starttime, false if not
    }

    public async Task<bool> AttendEventAsync(EventAttendance eventAttendance){
        if (CheckEventExistance(eventAttendance.Event_Id) && _checkAttendanceOnTime(eventAttendance.Event_Id)){
            List<EventAttendance> attendances = JsonFileHandler.ReadJsonFile<EventAttendance>("Data/EventAttendance.json");
            attendances.Add(eventAttendance);
            JsonFileHandler.WriteToJsonFile("Data/EventAttendance.json", attendances);
            return true;
        }
        return false;
    }


    private Event? _getEvent(Guid event_Id){
        List<Event> events = JsonFileHandler.ReadJsonFile<Event>("Data/Events.json");
        foreach (Event e in events){ // Q how to improve this?
            if (e.Id == event_Id){
                return e;
            }
        }
        return null;
    }

    public async Task<List<Guid>> ListEventAttendeesAsync(Guid event_Id){
        List<EventAttendance> eventAttendances = JsonFileHandler.ReadJsonFile<EventAttendance>("Data/EventAttendance.json");
        List<Guid> AttendeesIds = new List<Guid>();
        foreach (EventAttendance ea in eventAttendances){
            if (ea.Event_Id == event_Id){
                AttendeesIds.Add(ea.User_Id);
            }
        }
        return AttendeesIds;
    }

    public async Task<bool> DeleteEventAttendanceAsync(Guid event_Id, Guid user_id){
        List<EventAttendance> eventAttendances = JsonFileHandler.ReadJsonFile<EventAttendance>("Data/EventAttendance.json");
        foreach (EventAttendance ea in eventAttendances){
            if (ea.Event_Id == event_Id && ea.User_Id == user_id){
                eventAttendances.Remove(ea);
                JsonFileHandler.WriteToJsonFile("Data/EventAttendance.json", eventAttendances);
                return true;
            }
        }
        return false;
    }


}
