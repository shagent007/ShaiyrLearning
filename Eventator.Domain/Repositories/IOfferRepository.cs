using Eventator.Domain.Entities;

namespace Eventator.Domain.Repositories
{
    public interface IOfferRepository
    {
        List<Offer> GetList();
        Offer GetById(int id);
        Offer GetByScheduleId(int scheduleId);
        void Add(Offer model);
        void Update(Offer model);
        void Delete(int id);
    }

}
