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

        public void Add(Person person)
        {
            _dataManager.AddPerson(person);
        }

        public void Delete(int id)
        {
            _dataManager.Delete<Person>(id);
        }

        public void Update(Person person)
        {
            var _oldTickets = _dataManager.GetTickets(nameof(Ticket.PersonId),person.Id.ToString());
            var _newTickets = person.Tickets;

            var _removedTickets = GetRemovedTickets(_oldTickets, _newTickets);
            var _addedTickets = _newTickets.Where(t => !t.IsPersisted); 
            var _updatedTickets = GetUpdatedTickets(_oldTickets, _newTickets);

            foreach (var _removedticket in _removedTickets)
            {
                _dataManager.Delete<Ticket>(_removedticket.Id);
            }

            foreach (var _addedTicket in _addedTickets)
            {
                _dataManager.AddTicket(_addedTicket);
            }

            foreach (var _updatedTicket in _updatedTickets)
            {
                _dataManager.UpdateTicket(_updatedTicket);
            }

            _dataManager.UpdatePerson(person);
        }

        private List<Ticket> GetUpdatedTickets(List<Ticket> oldTickets, List<Ticket> newTickets)
        {
            var _updatedTickets = new List<Ticket>();
            foreach (var oldTicket in oldTickets)
            {
                var _ticket = newTickets.Find(t => t.Id == oldTicket.Id);
                if (_ticket == null) continue;
                if(_ticket.PersonId != oldTicket.PersonId || _ticket.ScheduleId != oldTicket.ScheduleId)
                {
                    _updatedTickets.Add(_ticket);
                }
            }
            return _updatedTickets;
        }

        private IEnumerable<Ticket> GetRemovedTickets(List<Ticket> oldTickets, List<Ticket> newTickets)
        {
            var _removedTickets = new List<Ticket>();

            foreach (var oldTicket in oldTickets)
            {
                if (!newTickets.Exists(t => t.Id == oldTicket.Id))
                {
                    _removedTickets.Add(oldTicket);
                }
            }

            return _removedTickets;
        }

        private Person LoadTickets(ref Person person)
        {
            var _tickets = _dataManager.GetTickets(nameof(Ticket.PersonId), person.Id.ToString());
            foreach (var ticket in _tickets)
            {
                person.Tickets.Add(ticket);
            }
            return person;
        }

        public Person GetById(int id)
        {
            var _person = _dataManager.GetPerson(nameof(Person.Id),id.ToString());
            return LoadTickets(ref _person);
        }

        public Person GetByName(string name)
        {
            var _person = _dataManager.GetPerson(nameof(Person.Name), name);
            return LoadTickets(ref _person);
        }

        public Person GetByEmail(string email)
        {
            var _person = _dataManager.GetPerson(nameof(Person.Email), email);
            return LoadTickets(ref _person);
        }

        public List<Person> GetList()
        {
            return _dataManager.GetPersons();
        }
    }
}