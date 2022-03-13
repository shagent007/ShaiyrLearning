using Eventator.DataContext.MsSql.Factories;
using Eventator.Domain.Entities;
using Eventator.Domain.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Eventator.DataContext.MsSql
{
    public class MsSqlScheduleRepository : IScheduleRepository
    {
        private readonly MsSqlDataBaseExecutor _dataBaseExecutor;
        private readonly ScheduleFactory _scheduleFactory;
        public MsSqlScheduleRepository(MsSqlDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
            _scheduleFactory = new ScheduleFactory();
        }

        private void Write(string sqlCommand)
        {
            _dataBaseExecutor.Execute(connection =>
            {
                var command = new SqlCommand(sqlCommand, connection);
                command.ExecuteNonQuery();
            });
        }

        private Schedule Read(string sqlCommand)
        {
            return _dataBaseExecutor.Execute<Schedule>(connection =>
            {
                var command = new SqlCommand(sqlCommand, connection);
                var reader = command.ExecuteReader();
                if (!reader.HasRows) return null;
                reader.Read();
                return _scheduleFactory.Create(reader);
            });
        }


        public void Add(Schedule model)
        {
            Write($"INSERT INTO {nameof(Schedule)} ({nameof(Schedule.EventId)}, {nameof(Schedule.StartDate)}) VALUES ( {model.EventId}, '{model.StartDate}')");
        }

        public void Delete(int id)
        {
            Write($"DELETE Schedule WHERE {nameof(Schedule.Id)}={id}");
        }

        public void Update(Schedule model)
        {
            Write($"UPDATE {nameof(Schedule)} SET {nameof(Schedule.EventId)}='{model.EventId}',{nameof(Schedule.StartDate)}='{model.StartDate}' WHERE {nameof(Schedule.Id)}={model.Id}");
        }

        public Schedule GetById(int id)
        {
            return Read($"SELECT  {nameof(Schedule.Id)},{nameof(Schedule.EventId)}, {nameof(Schedule.StartDate)} FROM {nameof(Schedule)} WHERE {nameof(Schedule.Id)}={id}");
        }


        public List<Schedule> GetList()
        {
            var sqlCommand = $"SELECT {nameof(Schedule.Id)},{nameof(Schedule.EventId)}, {nameof(Schedule.StartDate)} FROM {nameof(Schedule)}";
            return _dataBaseExecutor.Execute<List<Schedule>>(connection =>
            {
                var command = new SqlCommand(sqlCommand, connection);
                var reader = command.ExecuteReader();
                if (!reader.HasRows) return null;
                List<Schedule> _list = new List<Schedule>();

                while (reader.Read())
                {
                    var Schedule = _scheduleFactory.Create(reader);
                    _list.Add(Schedule);
                }

                return _list;
            });
        }
    }
}

