namespace Eventator.Domain.Entities
{
    public class Event : Entity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Schedule>? Schedules { get; set; }

        public Event() :base()
        {
            Schedules = new List<Schedule>();
        }
    }
}