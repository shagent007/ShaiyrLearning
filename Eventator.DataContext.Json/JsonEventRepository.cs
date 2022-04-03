using Eventator.Domain.Entities;
using Eventator.Domain.Repositories;

namespace Eventator.DataContext.Json
{
    public class JsonEventRepository : IEventRepository
    {
        private readonly JsonDataManager _dataManager;

        public JsonEventRepository(JsonDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Event LoadSchedules(Event _event)
        {
            var _shcedules = _dataManager.GetEntities<Schedule>(nameof(Schedule.EventId), _event.Id.ToString());
            foreach (var _schedule in _shcedules)
            {
                _event.Schedules.Add(_schedule);
            }
            return _event;
        }

        public Event GetById(int eventId)
        {
            var _event = _dataManager.GetEntity<Event>(nameof(Event.Id), eventId.ToString());
            return LoadSchedules(_event);
        }

        public List<Event> GetList()
        {
            return _dataManager.GetEntities<Event>();
        }

        public void Add(Event model)
        {
            _dataManager.Add(model);
        }

        public void Update(Event model)
        {
            _dataManager.Update(model);
        }

        public void Delete(int id)
        {
            _dataManager.Delete<Event>(id);
        }
    }
}
