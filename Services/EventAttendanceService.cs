
public class EventServices {
    // write attendance to attendance.json
    // check availabilty of attendance
    public bool CheckEventExistance(Guid event_Id){
        List<Event> events = JsonFileHandler.ReadJsonFile<Event>("Data/Events.json");
        foreach (Event e in events){ // Q how to improve this?
            if (e.Id == event_Id){
                return true;
            }
        }
        return false;
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
