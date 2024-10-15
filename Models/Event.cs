namespace OfficeTeam2_.Models
{
    public class Event
    {
        public Guid Id { get; set; } // should be Guid
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly Start_Time { get; set; }
        public TimeOnly End_Time { get; set; }
        public string Location { get; set; }
        public bool Admin_Approval { get; set; }

        public Event() { }

        public Event(Guid id, string title, string description, DateTime date, TimeOnly start_time, TimeOnly end_time, string location, bool admin_approval)
        {
            Id = id;
            Title = title;
            Description = description;
            Date = date;
            Start_Time = start_time;
            End_Time = end_time;
            Location = location;
            Admin_Approval = admin_approval;
        }
    }
}
