using Eventator.Domain.Models;
using Eventator.Domain.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Eventator.DataContext
{
    public class PersonRepository : IRepository<Person>
    {
        private readonly DataBaseExecutor _dataBaseExecutor;
        public PersonRepository(DataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public void Add(Person model)
        {
            _dataBaseExecutor.Execute(connection =>
            {
                SqlCommand command = new SqlCommand($"INSERT INTO Person (Name, Age, Email) VALUES ('{model.Name}', {model.Age}, '{model.Email}')", connection);
                command.ExecuteNonQuery();
            });
        }

        public void Delete(int id)
        {
            _dataBaseExecutor.Execute(connection =>
            {
                SqlCommand command = new SqlCommand($"DELETE Person WHERE Id={id}", connection);
                command.ExecuteNonQuery();
            });
        }

        public Person Get(int id)
        {
            return _dataBaseExecutor.Execute<Person>(connection =>
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Person WHERE Id={id}", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Person person = new Person();
                    person.Id = reader.GetInt32(0);
                    person.Age = reader.GetInt32(1);
                    person.Name = reader.GetString(2);
                    return person;
                }
                return null;
            });
        }

        public void Update(Person model)
        {
            _dataBaseExecutor.Execute(connection =>
            {
                SqlCommand command = new SqlCommand($"UPDATE Person SET Age={model.Age},Name={model.Name},Email={model.Email} WHERE Id={model.Id}", connection);
                command.ExecuteNonQuery();
            });
        }
    }
}

