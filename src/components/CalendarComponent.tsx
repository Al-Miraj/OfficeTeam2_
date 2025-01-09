import React, { useState } from "react";
import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";
import "./Calendar.css";

type Value = Date | [Date, Date] | null;

interface Event {
  name: string;
  date: Date;
}

const CalendarComponent: React.FC = () => {
  const [date, setDate] = useState<Value>(new Date());
  const [events, setEvents] = useState<Event[]>([]);
  const [eventName, setEventName] = useState<string>("");
  const [selectedDate, setSelectedDate] = useState<Date | null>(null);

  // Functie om de datum te selecteren voor het event
  const handleDateChange = (value: Value) => {
    setDate(value);
    if (Array.isArray(value)) {
      setSelectedDate(value[0]); 
    } else if (value instanceof Date) {
      setSelectedDate(value); 
    }
  };

  // Functie om het event toe te voegen
  const handleEventSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    if (selectedDate && eventName) {
      const newEvent: Event = { name: eventName, date: selectedDate };
      setEvents([...events, newEvent]);
      setEventName(""); 
    }
  };

  return (
    <div className="calendar-container">
      <h1 className="text-center">React Calendar</h1>
      <Calendar
        onChange={handleDateChange}
        value={date}
        selectRange={false}
        locale="en-US"  
      />
      <form onSubmit={handleEventSubmit} className="event-form">
        <input
          type="text"
          placeholder="Event Name"
          value={eventName}
          onChange={(e) => setEventName(e.target.value)}
          required
        />
        <button type="submit">Add Event</button>
      </form>

      <div className="event-list">
        <h3>Events:</h3>
        {events.map((event, index) => (
          <div key={index}>
            <p>
              {event.name} - {event.date.toDateString()}
            </p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default CalendarComponent;
