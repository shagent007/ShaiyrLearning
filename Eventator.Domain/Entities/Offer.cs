namespace Eventator.Domain.Entities
{
    public class Offer : Entity
    {
        public Schedule? Schedule { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}