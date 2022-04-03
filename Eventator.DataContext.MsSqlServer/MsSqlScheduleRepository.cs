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

        private Schedule LoadTickets(Schedule schedule)
        {
            var _tickets = _dataManager.GetTickets(nameof(Ticket.ScheduleId), schedule.Id.ToString());
            foreach (var ticket in _tickets)
            {
                schedule.Tickets.Add(ticket);
            }
            return schedule;
        }

        public void Add(Schedule model)
        {
            _dataManager.AddSchedule(model);
        }

        public void Delete(int id)
        {
            _dataManager.Delete<Schedule>(id);
        }

        public void Update(Schedule model)
        {
            _dataManager.UpdateSchedule(model);
        }

        public Schedule GetById(int id)
        {
            var _schedule = _dataManager.GetSchedule(nameof(Schedule.Id), id.ToString());
            _schedule.Event = _dataManager.GetEvent(nameof(Event.Id), _schedule.EventId.ToString());
            return LoadTickets(_schedule);
        }

        public List<Schedule> GetListByEventId(int eventId)
        {
            return _dataManager.GetSchedules(nameof(Schedule.EventId), eventId.ToString());
        }

        public List<Schedule> GetList()
        {
            return _dataManager.GetSchedules();
        }
    }
}