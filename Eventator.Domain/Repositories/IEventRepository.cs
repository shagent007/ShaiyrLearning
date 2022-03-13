using Eventator.Domain.Entities;

namespace Eventator.Domain.Repositories
{
    public interface IEventRepository 
    {
        List<Event> GetList();
        Event GetById(int id);
        void Add(Event model);
        void Update(Event model);
        void Delete(int id);
    }

}
