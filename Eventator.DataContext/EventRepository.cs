﻿using Eventator.Domain.Models;
using Eventator.Domain.Repositories;
using Microsoft.Data.SqlClient;

namespace Eventator.DataContext
{
    public class EventRepository : IRepository<Event>
    {
        private readonly DataBaseExecutor _dataBaseExecutor;
        public EventRepository(DataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
        }

        public void Add(Event model)
        {
            _dataBaseExecutor.Execute(connection =>
            {
                SqlCommand command = new SqlCommand($"INSERT INTO Event (Name, Description) VALUES ('{model.Name}', '{model.Description}')", connection);
                command.ExecuteNonQuery();
            });
        }

        public void Delete(int id)
        {
            _dataBaseExecutor.Execute(connection =>
            {
                SqlCommand command = new SqlCommand($"DELETE Event WHERE Id={id}", connection);
                command.ExecuteNonQuery();
            });
        }

        private Event CreateEvent(SqlDataReader dataReader)
        {
            while (dataReader.Read())
            {
                Event _event = new Event();
                _event.Id = dataReader.GetInt32(0);
                _event.Name = dataReader.GetString(1);
                _event.Description = dataReader.GetString(2);
                return _event;
            }
            return null;
        }

        public Event Get(int id)
        {
            return _dataBaseExecutor.Execute<Event>(connection =>
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Event WHERE Id={id}", connection);
                SqlDataReader reader = command.ExecuteReader();
                return CreateEvent(reader);
            });
        }

        public void Update(Event model)
        {
            _dataBaseExecutor.Execute(connection =>
            {
                SqlCommand command = new SqlCommand($"UPDATE Event SET Name={model.Name},Description={model.Description} WHERE Id={model.Id}", connection);
                command.ExecuteNonQuery();
            });
        }
    }
}

