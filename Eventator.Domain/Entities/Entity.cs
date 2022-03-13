namespace Eventator.Domain.Entities
{
    public abstract class Entity
    {
        private static int IdCounter = -1;

        public int Id { get; set; }

        public bool IsPersisted => Id >= 0;

        protected Entity()
        {
            Id = IdCounter;
            IdCounter--;
        }
    }
}