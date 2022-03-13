using Eventator.Domain.Entities;

namespace Eventator.Domain.Repositories
{
    public interface IPersonRepository 
    {
        List<Person> GetList();
        Person GetById(int id);
        void Add(Person model);
        void Update(Person model);
        void Delete(int id);
        Person GetByName(string name);
        Person GetByEmail(string email);
    }

}
