using Eventator.DataContext.MsSqlServer;
using Eventator.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eventator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly MsSqlPersonRepository _personRepository;

        public PersonsController()
        {
            var _executor = new MsSqlDataBaseExecutor("Data Source = 195.133.144.133, 49158; Database = Eventator_SHAIR; User ID = Shair; Password = 1234; TrustServerCertificate = true");
            var _dataManager = new MsSqlDataManager(_executor);
            _personRepository = new MsSqlPersonRepository(_dataManager);
        }

        [HttpPost]
        public void Add([FromBody] Person person) => _personRepository.Add(person);

        [HttpPut]
        public void Update([FromBody] Person person) => _personRepository.Update(person);

        [HttpGet("{name}")]
        public Person GetById(string name) => _personRepository.GetByName(name);

        [HttpDelete("{id}")]
        public void DeleteById(int id) => _personRepository.Delete(id);

        [HttpGet]
        public List<Person> GetList() => _personRepository.GetList();

    }
}
