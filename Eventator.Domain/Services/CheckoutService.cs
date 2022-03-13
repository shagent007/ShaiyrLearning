using Eventator.Domain.Entities;
using Eventator.Domain.Repositories;

namespace Eventator.Domain.Services
{
    public class CheckoutService
    {
        private readonly IOfferRepository _offerRepository;

        public CheckoutService(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public decimal CalculateTotal(Person customer, IEnumerable<Schedule> schedules)
        {
            decimal total = 0;
            Dictionary<Schedule, int> groups = schedules
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());

            foreach (var schedule in groups.Keys)
            {
                var totalTicketsCount = groups[schedule]; // 10

                var offer = _offerRepository.GetByScheduleId(schedule.Id);
                if (offer != null)
                {
                    var offersCount = totalTicketsCount / offer.Count; // 0

                    decimal offerTotal = offersCount * offer.Price; // 810 x 9

                    total += offerTotal; // 810

                    totalTicketsCount = totalTicketsCount % offer.Count; // 1
                }

                total += totalTicketsCount * schedule.Price;
            }

            return total;
        }
    }
}
