using Eventator.DataContext.MsSql.Factories;
using Eventator.Domain.Entities;
using Eventator.Domain.Repositories;
using Microsoft.Data.SqlClient;

namespace Eventator.DataContext.MsSql
{
    public class MsSqlEventRepository : IRepository<Event>
    {
        private readonly MsSqlDataBaseExecutor _dataBaseExecutor;
        private readonly EventFactory _factory;
        public MsSqlEventRepository(MsSqlDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
            _factory = new EventFactory();
        }

        private void Write(string sqlCommand)
        {
            _dataBaseExecutor.Execute(connection =>
            {
                var command = new SqlCommand(sqlCommand, connection);
                command.ExecuteNonQuery();
            });
        }

        private Event Read(string sqlCommand)
        {
            return _dataBaseExecutor.Execute<Event>(connection =>
            {
                var command = new SqlCommand(sqlCommand, connection);
                var reader = command.ExecuteReader();
                if (!reader.HasRows) return null;
                reader.Read();
                return _factory.Create(reader);
            });
        }


        public void Add(Event model)
        {
            Write($"INSERT INTO {nameof(Event)} ({nameof(Event.Name)}, {nameof(Event.Description)}) VALUES ('{model.Name}', '{model.Description}')");
        }

        public void Delete(int id)
        {
            Write($"DELETE {nameof(Event)} WHERE {nameof(Event.Id)}={id}");
        }

        public void Update(Event model)
        {
            Write($"UPDATE {nameof(Event)} SET {nameof(Event.Description)}={model.Description},{nameof(Event.Name)}={model.Name} WHERE {nameof(Event.Id)}={model.Id}");
        }

        public Event GetById(int id)
        {
            return Read($"SELECT {nameof(Event.Id)},{nameof(Event.Name)}, {nameof(Event.Description)} FROM {nameof(Event)} WHERE {nameof(Event.Id)}={id}");
        }

        public Event GetByName(string name)
        {
            return Read($"SELECT {nameof(Event.Id)},{nameof(Event.Name)}, {nameof(Event.Description)}  FROM {nameof(Event)} WHERE {nameof(Event.Name)}={name}");
        }

        public List<Event> GetList()
        {
            var sqlCommand = $"SELECT {nameof(Event.Id)},{nameof(Event.Name)}, {nameof(Event.Description)} FROM {nameof(Event)}";
            return _dataBaseExecutor.Execute<List<Event>>(connection =>
            {
                var command = new SqlCommand(sqlCommand, connection);
                var reader = command.ExecuteReader();
                if (!reader.HasRows) return null;
                var _list = new List<Event>();

                while (reader.Read())
                {
                    _list.Add(_factory.Create(reader));
                }

                return _list;
            });
        }
    }
}

