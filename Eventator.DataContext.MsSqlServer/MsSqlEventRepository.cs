using Eventator.DataContext.MsSqlServer.Factories;
using Eventator.Domain.Entities;
using Eventator.Domain.Repositories;

namespace Eventator.DataContext.MsSqlServer
{
    public class MsSqlEventRepository : IEventRepository
    {
        private readonly MsSqlDataManager _dataManager;
        public MsSqlEventRepository(MsSqlDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        private Event LoadSchedules(Event _event)
        {
            var _schedules = _dataManager.GetSchedules(nameof(Schedule.EventId), _event.Id.ToString());
            foreach (var schedule in _schedules)
            {
                _event.Schedules.Add(schedule);
            }
            return _event;
        }

        public void Add(Event model)
        {
            _dataManager.AddEvent(model);
        }

        public void Delete(int id)
        {
            _dataManager.Delete<Event>(id);
        }

        public void Update(Event model)
        {
            _dataManager.UpdateEvent(model);
        }

        public Event GetById(int id)
        {
            var _event = _dataManager.GetEvent(nameof(Event.Id), id.ToString());
            return LoadSchedules( _event);
        }

        public Event GetByName(string name)
        {
            var _event = _dataManager.GetEvent(nameof(Event.Name), name);
            return LoadSchedules( _event);
        }

        public List<Event> GetList()
        {
            return _dataManager.GetEvents();
        }
    }
}