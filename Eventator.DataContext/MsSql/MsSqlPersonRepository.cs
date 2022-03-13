using Eventator.DataContext.MsSql.Factories;
using Eventator.Domain.Entities;
using Eventator.Domain.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Eventator.DataContext.MsSql
{
    public class MsSqlPersonRepository : IRepository<Person>
    {
        private readonly MsSqlDataBaseExecutor _dataBaseExecutor;
        private readonly PersonFactory _personFactory;
        public MsSqlPersonRepository(MsSqlDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
            _personFactory = new PersonFactory();
        }

        private void Write(string sqlCommand)
        {
            _dataBaseExecutor.Execute(connection =>
            {
                var command = new SqlCommand(sqlCommand, connection);
                command.ExecuteNonQuery();
            });
        }

        private Person Read(string sqlCommand)
        {
            return _dataBaseExecutor.Execute<Person>(connection =>
            {
                var command = new SqlCommand(sqlCommand, connection);
                var reader = command.ExecuteReader();
                if (!reader.HasRows) return null;
                reader.Read();
                return _personFactory.Create(reader);
            });
        }


        public void Add(Person model)
        {
            Write($"INSERT INTO {nameof(Person)} ({nameof(Person.Name)}, {nameof(Person.Age)}, {nameof(Person.Email)}) VALUES ('{model.Name}', {model.Age}, '{model.Email}')");
        }

        public void Delete(int id)
        {
            Write($"DELETE Person WHERE {nameof(Person.Id)}={id}");
        }

        public void Update(Person model)
        {
            Write($"UPDATE {nameof(Person)} SET Age={model.Age},Name='{model.Name}',Email='{model.Email}' WHERE {nameof(Person.Id)}={model.Id}");
        }

        public Person GetById(int id)
        {
            return Read($"SELECT  {nameof(Person.Id)},{nameof(Person.Name)}, {nameof(Person.Age)}, {nameof(Person.Email)} FROM {nameof(Person)} WHERE {nameof(Person.Id)}={id}");
        }

        public Person GetByName(string name)
        {
            return Read($"SELECT {nameof(Person.Id)},{nameof(Person.Name)}, {nameof(Person.Age)}, {nameof(Person.Email)} FROM {nameof(Person)} WHERE {nameof(Person.Name)}={name}");
        }

        public Person GetByEmail(string email)
        {
            return Read($"SELECT {nameof(Person.Id)},{nameof(Person.Name)}, {nameof(Person.Age)}, {nameof(Person.Email)} FROM {nameof(Person)} WHERE {nameof(Person.Email)}={email}");
        }

        public List<Person> GetList()
        {
            var sqlCommand = $"SELECT {nameof(Person.Id)},{nameof(Person.Name)}, {nameof(Person.Age)}, {nameof(Person.Email)} FROM {nameof(Person)}";
            return _dataBaseExecutor.Execute<List<Person>>(connection =>
            {
                var command = new SqlCommand(sqlCommand, connection);
                var reader = command.ExecuteReader();
                if (!reader.HasRows) return null;
                List<Person> _list = new List<Person>();

                while (reader.Read())
                {
                    var person = _personFactory.Create(reader);
                    _list.Add(person);
                }

                return _list;
            });
        }
    }
}

