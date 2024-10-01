using System;
using System.Collections.Generic;

namespace EventManagementAPI.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Location { get; set; }

        // Collections of reviews and attendees
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Attendee> Attendees { get; set; } = new List<Attendee>();
    }

    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        
        public int EventId { get; set; }
        public Event Event { get; set; }
    }

    public class Attendee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
