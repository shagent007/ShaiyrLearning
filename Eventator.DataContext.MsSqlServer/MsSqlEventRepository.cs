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

        private Event LoadSchedules(ref Event _event)
        {
            var _schedules = _dataManager.GetSchedulesByField(nameof(Schedule.EventId), _event.Id.ToString());
            foreach (var schedule in _schedules)
            {
                _event.Schedules.Add(schedule);
            }
            return _event;
        }

        public void Add(Event model)
        {
            _dataManager.Write($"INSERT INTO {nameof(Event)} ({nameof(Event.Name)}, {nameof(Event.Description)}) VALUES ('{model.Name}', '{model.Description}')");
        }

        public void Delete(int id)
        {
            _dataManager.Write($"DELETE {nameof(Event)} WHERE {nameof(Event.Id)}={id}");
        }

        public void Update(Event model)
        {
            _dataManager.Write($"UPDATE {nameof(Event)} SET {nameof(Event.Description)}='{model.Description}',{nameof(Event.Name)}='{model.Name}' WHERE {nameof(Event.Id)}={model.Id}");
        }

        public Event GetById(int id)
        {
            var _event = _dataManager.GetEventByField(nameof(Event.Id), id.ToString());
            return LoadSchedules(ref _event);
        }

        public Event GetByName(string name)
        {
            var _event = _dataManager.GetEventByField(nameof(Event.Name), name);
            return LoadSchedules(ref _event);
        }

        public List<Event> GetList()
        {
            return _dataManager.GetEventList();
        }
    }
}