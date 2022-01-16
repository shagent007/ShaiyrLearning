namespace Eventator.Domain.Models
{
    public class Registration
    {
        public int PersonId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime CreateOn { get; set; }
        public int Price { get; set; }
    }
}