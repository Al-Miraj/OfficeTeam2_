import React, { useState } from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';

// Definieer het type zoals verwacht door react-calendar
type Value = Date | [Date, Date] | null;

const CalendarComponent: React.FC = () => {
  const [date, setDate] = useState<Value>(new Date());

  // Pas de handler aan zodat deze elk type waarde ondersteunt (Date, [Date, Date], of null)
  const handleDateChange = (value: Value) => {
    // Als de waarde een bereik is, moeten we ervoor zorgen dat het wordt behandeld als een array van datums.
    if (Array.isArray(value)) {
      const [startDate, endDate] = value;
      console.log("Selected range:", startDate, endDate); // Debugging: laat het geselecteerde bereik zien
    } else {
      console.log("Selected date:", value); // Debugging: laat de geselecteerde datum zien
    }
    setDate(value); // Update de geselecteerde datum of het bereik
  };

  return (
    <div className="calendar-container">
      <h1 className="text-center">React Calendar</h1>
      <Calendar
        onChange={handleDateChange} // De handler accepteert nu een Value type
        value={date}
        selectRange={true} // Voor datumselectie in een bereik
      />
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
    </div>
  );
};

export default CalendarComponent;
