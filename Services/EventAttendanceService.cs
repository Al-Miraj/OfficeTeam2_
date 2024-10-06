
public class EventServices {
    // write attendance to attendance.json
    // check availabilty of attendance
    public bool CheckEventExistance(Guid event_Id){
        Event? event_ = this.GetEvent(event_Id);
        return event_ is not null; // true if event exists, false if not
    }

    public bool CheckAttendanceOnTime(Guid event_Id){
        Event? event_ = this.GetEvent(event_Id);
        if (event_ is null) return false;

        TimeOnly now = TimeOnly.FromDateTime(DateTime.Now);
        return event_.Start_Time > now && event_.Date >= DateTime.Now; // true if its before the starttime, false if not
    }


    public Event? GetEvent(Guid event_Id){
        List<Event> events = JsonFileHandler.ReadJsonFile<Event>("Data/Events.json");
        foreach (Event e in events){ // Q how to improve this?
            if (e.Id == event_Id){
                return e;
            }
        }
        return null;
    }

    public List<Guid> ListEventAttendees(Guid event_Id){
        List<EventAttendance> eventAttendances = JsonFileHandler.ReadJsonFile<EventAttendance>("Data/EventAttendance.json");
        List<Guid> AttendeesIds = new List<Guid>();
        foreach (EventAttendance ea in eventAttendances){
            if (ea.Event_Id == event_Id){
                AttendeesIds.Add(ea.User_Id);
            }
        }
        return AttendeesIds;
    }

    public bool DeleteEventAttendance(Guid event_Id, Guid user_id){
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
