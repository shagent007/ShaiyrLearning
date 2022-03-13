using Eventator.DataContext.MsSqlServer;
using Eventator.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eventator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly MsSqlScheduleRepository _scheduleRepository;

        public ScheduleController()
        {
            var _executor = new MsSqlDataBaseExecutor("Data Source = 195.133.144.133, 49158; Database = Eventator_SHAIR; User ID = Shair; Password = 1234; TrustServerCertificate = true");
            var _dataManager = new MsSqlDataManager(_executor);
            _scheduleRepository = new MsSqlScheduleRepository(_dataManager);
        }

        [HttpPost]
        public void Add([FromBody] Schedule schedule) => _scheduleRepository.Add(schedule);

        [HttpGet("{id}")]
        public Schedule GetById(int id) => _scheduleRepository.GetById(id);
        [HttpPut("{id}")]
        public void Update([FromBody] Schedule schedule) => _scheduleRepository.Update(schedule);

        [HttpDelete("{id}")]
        public void DeleteById(int id) => _scheduleRepository.Delete(id);

        [HttpGet]
        public List<Schedule> GetList() => _scheduleRepository.GetList();

    }
}
