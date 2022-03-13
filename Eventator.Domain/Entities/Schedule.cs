using System.Text.Json.Serialization;

namespace Eventator.Domain.Entities
{
    public class Schedule : Entity
    {
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public decimal  Price { get; set; }
        public DateTime StartDate { get; set; }
        public List<Ticket> Tickets { get; set; }
        public int MaxCapacity { get; set; }
        public Schedule() : base()
        {
            Tickets = new List<Ticket>();
            MaxCapacity = 1;
            Price = 1;
        }
    }
}