using Eventator.Domain.Entities;
using Eventator.Domain.Repositories;

namespace Eventator.DataContext.Json
{
    public class JsonPersonRepository : IPersonRepository
    {

        private readonly JsonDataManager _dataManager;

        public JsonPersonRepository(JsonDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        private Person LoadTickets(Person person)
        {
            var _tickets = _dataManager.GetEntities<Ticket>(nameof(Ticket.PersonId), person.Id.ToString());
            foreach (var ticket in _tickets)
            {
                person.Tickets.Add(ticket);
            }
            return person;
        }

        public Person GetById(int personId)
        {
            var _person = _dataManager.GetEntity<Person>(nameof(Person.Id), personId.ToString());
            return LoadTickets(_person);
        }

        public List<Person> GetList()
        {
            return _dataManager.GetEntities<Person>();

        }

        public Person GetByName(string name)
        {
            var _person = _dataManager.GetEntity<Person>(nameof(Person.Name), name);
            return LoadTickets(_person);
        }

        public Person GetByEmail(string email)
        {
            var _person = _dataManager.GetEntity<Person>(nameof(Person.Email), email);
            return LoadTickets(_person);
        }

        public void Add(Person model)
        {
            _dataManager.Add(model);
        }

        public void Update(Person model)
        {
            _dataManager.Update(model);
        }

        public void Delete(int id)
        {
            _dataManager.Delete<Person>(id);
        }
    }
}
