// import React, { useState } from 'react';
// import Calendar from 'react-calendar';
// import 'react-calendar/dist/Calendar.css';

// // Definieer het type zoals verwacht door react-calendar
// type Value = Date | [Date, Date] | null;

// const CalendarComponent: React.FC = () => {
//   const [date, setDate] = useState<Value>(new Date());

//   // Pas de handler aan zodat deze elk type waarde ondersteunt (Date, [Date, Date], of null)
//   const handleDateChange = (value: Value) => {
//     // Als de waarde een bereik is, moeten we ervoor zorgen dat het wordt behandeld als een array van datums.
//     if (Array.isArray(value)) {
//       const [startDate, endDate] = value;
//       console.log("Selected range:", startDate, endDate); // Debugging: laat het geselecteerde bereik zien
//     } else {
//       console.log("Selected date:", value); // Debugging: laat de geselecteerde datum zien
//     }
//     setDate(value); // Update de geselecteerde datum of het bereik
//   };

//   return (
//     <div className="calendar-container">
//       <h1 className="text-center">React Calendar</h1>
//       <Calendar
//         onChange={handleDateChange} // De handler accepteert nu een Value type
//         value={date}
//         selectRange={true} // Voor datumselectie in een bereik
//       />
//       <p className="text-center">
//         {Array.isArray(date) && date.length === 2 ? (
//           <>
//             <span className="bold">Start:</span> {date[0].toDateString()} &nbsp;|&nbsp;
//             <span className="bold">End:</span> {date[1].toDateString()}
//           </>
//         ) : date instanceof Date ? (
//           <>
//             <span className="bold">Selected Date:</span> {date.toDateString()}
//           </>
//         ) : (
//           "No date selected"
//         )}
//       </p>
//     </div>
//   );
// };

// export default CalendarComponent;

// import React, { useState } from 'react';
// import Calendar from 'react-calendar';
// import 'react-calendar/dist/Calendar.css';
// import { useLocation } from "react-router-dom";

// type Value = Date | [Date, Date] | null;

// const CalendarComponent: React.FC = () => {
//   const [date, setDate] = useState<Value>(new Date());
//   const [events, setEvents] = useState<{ start: Date; end?: Date; title: string }[]>([]);
//   const [eventTitle, setEventTitle] = useState<string>("");
//   const [showEventForm, setShowEventForm] = useState(false);
//   const location = useLocation();
//   const user = location.state?.user; 

//   const handleDateChange = (value: Value) => {
//     setDate(value);
//     setShowEventForm(false); 
//   };

//   const handleAddEventClick = () => {
//     setShowEventForm(true); 
//   };

//   const handleSaveEvent = () => {
//     if (eventTitle.trim()) {
//       if (Array.isArray(date)) {
//         const [startDate, endDate] = date;
//         setEvents([...events, { start: startDate, end: endDate, title: eventTitle }]);
//       } else if (date instanceof Date) {
//         setEvents([...events, { start: date, title: eventTitle }]);
//       }
//       setEventTitle(""); 
//       setShowEventForm(false);
//     } else {
//       alert("Please enter a title for the event.");
//     }
//   };

//   return (
//     <div className="calendar-container">
//       <h1 className="text-center">React Calendar</h1>
//       <Calendar
//         onChange={handleDateChange}
//         value={date}
//         selectRange={true}
//       />
//       <p className="text-center">
//         {Array.isArray(date) && date.length === 2 ? (
//           <>
//             <span className="bold">Start:</span> {date[0].toDateString()} &nbsp;|&nbsp;
//             <span className="bold">End:</span> {date[1].toDateString()}
//           </>
//         ) : date instanceof Date ? (
//           <>
//             <span className="bold">Selected Date:</span> {date.toDateString()}
//           </>
//         ) : (
//           "No date selected"
//         )}
//       </p>
//       <button onClick={handleAddEventClick}>Add Event</button>

//       {showEventForm && (
//         <div className="event-form">
//           <h3>Add Event</h3>
//           <input
//             type="text"
//             placeholder="Event Title"
//             value={eventTitle}
//             onChange={(e) => setEventTitle(e.target.value)}
//           />
//           <button onClick={handleSaveEvent}>Save Event</button>
//         </div>
//       )}

//       <div className="event-list">
//         <h3>Events</h3>
//         {events.map((event, index) => (
//           <div key={index} className="event">
//             <p>
//               <strong>{event.title}</strong> | {event.start.toDateString()}
//               {event.end && ` - ${event.end.toDateString()}`}
//             </p>
//           </div>
//         ))}
//       </div>
//     </div>
//   );
// };

// export default CalendarComponent;


import React, { useState } from "react";
import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";
import { useLocation } from "react-router-dom";

type Value = Date | [Date, Date] | null;

const CalendarComponent: React.FC = () => {
  const [date, setDate] = useState<Value>(new Date());
  const [events, setEvents] = useState<{ start: Date; end?: Date; title: string }[]>([]);
  const [eventTitle, setEventTitle] = useState<string>("");
  const [showEventForm, setShowEventForm] = useState(false);
  const [loading, setLoading] = useState(false); // Track request status
  const [error, setError] = useState<string | null>(null); // Track errors
  const location = useLocation();
  const user = location.state?.user;

  // Function to handle date changes
  const handleDateChange = (value: Value) => {
    setDate(value);
    setShowEventForm(false);
  };

  // Show the event form
  const handleAddEventClick = () => {
    setShowEventForm(true);
  };

  // Function to send event data to the server
  const saveEventToServer = async (eventData: {
    Title: string;
    Date: string;
    Start_Time: string;
    End_Time?: string;
    Location: string;
    Admin_Approval: boolean;
  }) => {
    try {
      setLoading(true); // Set loading state
      setError(null); // Clear previous errors
      const response = await fetch("https://localhost:5000/events", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(eventData),
      });

      if (!response.ok) {
        throw new Error("Failed to save the event. Please try again.");
      }

      const savedEvent = await response.json();
      console.log("Event saved:", savedEvent);

      setEvents((prevEvents) => [
        ...prevEvents,
        {
          start: new Date(savedEvent.Date),
          end: savedEvent.End_Time ? new Date(savedEvent.Date) : undefined,
          title: savedEvent.Title,
        },
      ]);

      alert("Event saved successfully!"); // Feedback to the user
    } catch (err: any) {
      setError(err.message); // Set error state
      console.error("Error saving event:", err);
    } finally {
      setLoading(false); // Reset loading state
    }
  };

  // Save the event locally and send to the server
  const handleSaveEvent = async () => {
    if (eventTitle.trim()) {
      if (date) {
        const startDate = Array.isArray(date) ? date[0] : date;
        const endDate = Array.isArray(date) ? date[1] : null;

        // Create the event data
        const newEvent = {
          Title: eventTitle,
          Date: startDate.toISOString(), // Use ISO string for consistent date format
          Start_Time: "09:00:00", // Default start time (you can customize this)
          End_Time: endDate ? "17:00:00" : undefined, // Default end time
          Location: "Default Location", // Placeholder location
          Admin_Approval: true, // Default approval (change based on your requirements)
        };

        // Update local state for UI
        setEvents([
          ...events,
          { start: startDate, end: endDate, title: eventTitle },
        ]);
        setEventTitle("");
        //setShowEventForm(false);

        // Send the event to the server
        await saveEventToServer(newEvent);
      } else {
        alert("Please select a date for the event.");
      }
    } else {
      alert("Please enter a title for the event.");
    }
  };

  return (
    <div className="calendar-container">
      <h1 className="text-center">React Calendar</h1>
      <Calendar onChange={handleDateChange} value={date} selectRange={true} />
      <p className="text-center">
        {Array.isArray(date) && date.length === 2 ? (
          <>
            <span className="bold">Start:</span> {date[0].toDateString()} &nbsp;|&nbsp;
            <span className="bold">End:</span> {date[1].toDateString()}
          </>
        ) : date instanceof Date ? (
          <>
            <span className="bold">Selected Date:</span> {date.toDateString()}
          </>
        ) : (
          "No date selected"
        )}
      </p>
      <button onClick={handleAddEventClick}>Add Event</button>

      {showEventForm && (
        <div className="event-form">
          <h3>Add Event</h3>
          <input
            type="text"
            placeholder="Event Title"
            value={eventTitle}
            onChange={(e) => setEventTitle(e.target.value)}
          />
          <button onClick={handleSaveEvent} disabled={loading}>
            {loading ? "Saving..." : "Save Event"}
          </button>
          {error && <p className="error">{error}</p>} {/* Display error if any */}
        </div>
      )}

      <div className="event-list">
        <h3>Events</h3>
        {events.map((event, index) => (
          <div key={index} className="event">
            <p>
              <strong>{event.title}</strong> | {event.start.toDateString()}
              {event.end && ` - ${event.end.toDateString()}`}
            </p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default CalendarComponent;
