using Eventator.Domain.Entities;
using Eventator.Domain.Repositories;

namespace Eventator.DataContext.Json
{
    public class JsonScheduleRepository : IScheduleRepository
    {
        private readonly JsonDataManager _dataManager;

        public JsonScheduleRepository(JsonDataManager dataManager)
        {
            _dataManager = dataManager;
        }
        private Schedule LoadTickets(Schedule schedule)
        {
            var _tickets = _dataManager.GetEntities<Ticket>(nameof(Ticket.ScheduleId), schedule.Id.ToString());
            foreach (var ticket in _tickets)
            {
                schedule.Tickets.Add(ticket);
            }
            return schedule;
        }

        public Schedule GetById(int scheduleId)
        {
            var _schedule = _dataManager.GetEntity<Schedule>(nameof(Schedule.Id), scheduleId.ToString());
            return LoadTickets(_schedule);
        }

        public List<Schedule> GetList()
        {
            return _dataManager.GetEntities<Schedule>();
        }
        public void Add(Schedule model)
        {
            _dataManager.Add(model);
        }

        public void Update(Schedule model)
        {
            _dataManager.Update(model);
        }

        public void Delete(int id)
        {
            _dataManager.Delete<Schedule>(id);
        }

        public List<Schedule> GetListByEventId(int eventId)
        {
            return _dataManager.GetEntities<Schedule>(nameof(Schedule.EventId), eventId.ToString());
        }
    }
}
