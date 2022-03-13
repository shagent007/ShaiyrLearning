using Eventator.Domain.Entities;
using Eventator.Domain.Repositories;

namespace Eventator.DataContext.MsSqlServer
{
    public class MsSqlScheduleRepository : IScheduleRepository
    {
        private readonly MsSqlDataManager _dataManager;
        public MsSqlScheduleRepository(MsSqlDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        private Schedule LoadTickets(ref Schedule schedule)
        {
            var _tickets = _dataManager.GetTicketsByField(nameof(Ticket.ScheduleId), schedule.Id.ToString());
            foreach (var ticket in _tickets)
            {
                schedule.Tickets.Add(ticket);
            }
            return schedule;
        }

        public void Add(Schedule model)
        {
            _dataManager.Write(@$"
                INSERT INTO 
                    {nameof(Schedule)} 
                    (
                        {nameof(Schedule.EventId)}, 
                        {nameof(Schedule.StartDate)},
                        {nameof(Schedule.Price)},
                        {nameof(Schedule.MaxCapacity)}
                    ) 
                VALUES 
                    ( 
                        {model.EventId}, 
                        '{model.StartDate}',
                        {model.Price},
                        {model.MaxCapacity}
                    )
            ");
        }

        public void Delete(int id)
        {
            _dataManager.Write($"DELETE Schedule WHERE {nameof(Schedule.Id)}={id}");
        }

        public void Update(Schedule model)
        {
            _dataManager.Write(@$"
                UPDATE 
                    {nameof(Schedule)} 
                SET 
                    {nameof(Schedule.EventId)}='{model.EventId}',
                    {nameof(Schedule.StartDate)}='{model.StartDate}',
                    {nameof(Schedule.Price)}={model.Price},
                    {nameof(Schedule.MaxCapacity)}={model.MaxCapacity}
                WHERE 
                    {nameof(Schedule.Id)}={model.Id}
            ");
        }

        public Schedule GetById(int id)
        {
            var _schedule = _dataManager.GetScheduleByField(nameof(Person.Id), id.ToString());
            _schedule.Event = _dataManager.GetEventByField(nameof(Event.Id), _schedule.EventId.ToString());
            return LoadTickets(ref _schedule);
        }

        public List<Schedule> GetListByEventId(int eventId)
        {
            return _dataManager.GetScheduleListByField(nameof(Schedule.EventId), eventId.ToString());
        }

        public List<Schedule> GetList()
        {
            return _dataManager.GetScheduleList();
        }
    }
}