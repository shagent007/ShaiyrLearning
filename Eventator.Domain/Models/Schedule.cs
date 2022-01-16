namespace Eventator.Domain.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public DateTime StartDate { get; set; }
    }
}