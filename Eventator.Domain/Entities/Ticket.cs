using System.Text.Json.Serialization;

namespace Eventator.Domain.Entities
{
    public class Ticket : Entity
    {
        public int PersonId { get; set; }
        public int ScheduleId { get; set; }
        public Person? Person { get; set; }
        public Schedule? Schedule { get; set; }  
        public DateTime CreateOn { get; set; }

        public Ticket() : base()
        {

        }
    }
}