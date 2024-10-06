namespace OfficeTeam2_.Models
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
        public List<string> Reviews { get; set; } = new List<string>();
        public List<string> Attendees { get; set; } = new List<string>();
    }
}
