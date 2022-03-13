using Eventator.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Eventator.DataContext.MsSql.Factories
{
    public class PersonFactory
    {
        public Person Create(SqlDataReader reader)
        {
            var _person = new Person();
            _person.Id = reader.GetInt32(nameof(_person.Id));
            _person.Age = reader.GetInt32(nameof(_person.Age));
            _person.Name = reader.GetString(nameof(_person.Name));
            _person.Email = reader.GetString(nameof(_person.Email));
            return _person;
        }
    }
}

