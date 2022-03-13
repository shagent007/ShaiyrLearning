using Eventator.DataContext.MsSqlServer;
using Eventator.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Eventator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly MsSqlEventRepository _eventRepository;

        public EventsController()
        {
            var _executor = new MsSqlDataBaseExecutor("Data Source = 195.133.144.133, 49158; Database = Eventator_SHAIR; User ID = Shair; Password = 1234; TrustServerCertificate = true");
            var _dataManager = new MsSqlDataManager(_executor);
            _eventRepository = new MsSqlEventRepository(_dataManager);
        }

        [HttpPost]
        public void Add([FromBody] Event Event) => _eventRepository.Add(Event);

        [HttpPut]
        public void Update([FromBody] Event Event) => _eventRepository.Update(Event);

        [HttpGet("{id}")]
        public Event GetById(int id) => _eventRepository.GetById(id);

        [HttpDelete("{id}")]
        public void DeleteById(int id) => _eventRepository.Delete(id);

        [HttpGet]
        public List<Event> GetList() => _eventRepository.GetList();

    }
}
