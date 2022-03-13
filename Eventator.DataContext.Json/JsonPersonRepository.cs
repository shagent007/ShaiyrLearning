using Eventator.Domain.Entities;
using Eventator.Domain.Repositories;

namespace Eventator.DataContext.Json
{
    public class JsonPersonRepository : IPersonRepository
    {

        private readonly JsonDataManager _dataManager;

        public JsonPersonRepository(JsonDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Person GetById(int personId)
        {
            return _dataManager.GetPerson(nameof(Person.Id), personId.ToString()); 
        }

        public List<Person> GetList()
        {
            return _dataManager.GetPeople(); 
        }

        public Person GetByName(string name)
        {
            return _dataManager.GetPerson(nameof(Person.Name), name);
        }

        public Person GetByEmail(string email)
        {
            return _dataManager.GetPerson(nameof(Person.Email), email);
        }

        public void Add(Person model)
        {
            _dataManager.Add(model);
        }

        public void Update(Person model)
        {
            _dataManager.Update(model);
        }

        public void Delete(int id)
        {
            _dataManager.Delete<Person>(id);
        }
    }
}
