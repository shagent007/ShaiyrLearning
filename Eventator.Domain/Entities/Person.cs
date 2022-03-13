namespace Eventator.Domain.Entities
{
    public class Person : Entity
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public List<Ticket> Tickets { get; set; }
        public Person():base()
        {
            Tickets = new List<Ticket>();
        }
    }
}