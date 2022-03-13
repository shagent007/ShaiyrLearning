using Eventator.Domain.Entities;
using Eventator.Domain.Repositories;

namespace Eventator.DataContext.MsSqlServer
{
    public class MsSqlPersonRepository : IPersonRepository
    {
        private readonly MsSqlDataManager _dataManager;
        public MsSqlPersonRepository(MsSqlDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public void Add(Person model)
        {
            _dataManager.Write($"INSERT INTO {nameof(Person)} ({nameof(Person.Name)}, {nameof(Person.Age)}, {nameof(Person.Email)}) VALUES ('{model.Name}', {model.Age}, '{model.Email}')");
        }

        public void Delete(int id)
        {
            _dataManager.Write($"DELETE {nameof(Person)} WHERE {nameof(Person.Id)}={id}");
        }

        public void Update(Person model)
        {
            _dataManager.Write($"UPDATE {nameof(Person)} SET {model.Age}={model.Age},{model.Name}='{model.Name}',{model.Email}='{model.Email}' WHERE {nameof(Person.Id)}={model.Id}");
        }

        private Person LoadTickets(ref Person person)
        {
            var _tickets = _dataManager.GetTicketsByField(nameof(Ticket.PersonId), person.Id.ToString());
            foreach (var ticket in _tickets)
            {
                person.Tickets.Add(ticket);
            }
            return person;
        }

        public Person GetById(int id)
        {
            var _person = _dataManager.GetPersonByField(nameof(Person.Id),id.ToString());
            return LoadTickets(ref _person);
        }

        public Person GetByName(string name)
        {
            var _person = _dataManager.GetPersonByField(nameof(Person.Name), name);
            return LoadTickets(ref _person);
        }

        public Person GetByEmail(string email)
        {
            var _person = _dataManager.GetPersonByField(nameof(Person.Email), email);
            return LoadTickets(ref _person);
        }

        public List<Person> GetList()
        {
            return _dataManager.GetPersonList();
        }
    }
}