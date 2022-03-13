using Eventator.Domain.Entities;

namespace Eventator.Domain.Repositories
{
    public interface IScheduleRepository
    {
        List<Schedule> GetList();
        List<Schedule> GetListByEventId(int eventId);
        Schedule GetById(int id);
        void Add(Schedule model);
        void Update(Schedule model);
        void Delete(int id);
    }

}
