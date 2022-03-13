using Eventator.DataContext.MsSql.Factories;
using Eventator.Domain.Entities;
using Eventator.Domain.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Eventator.DataContext.MsSql
{
    public class MsSqlRegistrationRepository : IRegistrationRepository
    {
        private readonly MsSqlDataBaseExecutor _dataBaseExecutor;
        private readonly RegistrationFactory _registrationFactory;
        public MsSqlRegistrationRepository(MsSqlDataBaseExecutor dataBaseExecutor)
        {
            _dataBaseExecutor = dataBaseExecutor;
            _registrationFactory = new RegistrationFactory();
        }

        private void Write(string sqlCommand)
        {
            _dataBaseExecutor.Execute(connection =>
            {
                var command = new SqlCommand(sqlCommand, connection);
                command.ExecuteNonQuery();
            });
        }

        private Registration Read(string sqlCommand)
        {
            return _dataBaseExecutor.Execute<Registration>(connection =>
            {
                var command = new SqlCommand(sqlCommand, connection);
                var reader = command.ExecuteReader();
                if (!reader.HasRows) return null;
                reader.Read();
                return _registrationFactory.Create(reader);
            });
        }


        public void Add(Registration model)
        {
            Write($"INSERT INTO {nameof(Registration)} ({nameof(Registration.PersonId)},{nameof(Registration.PersonId)}, {nameof(Registration.ScheduleId)}, {nameof(Registration.CreateOn)},{nameof(Registration.Price)}) VALUES ({model.Id},{model.PersonId}, {model.ScheduleId}, {model.CreateOn}, {model.Price})");
        }

        public void Delete(int id)
        {
            Write($"DELETE Registration WHERE {nameof(Registration.Id)}={id}");
        }

        public void Update(Registration model)
        {
            Write($"UPDATE {nameof(Registration)} SET {nameof(Registration.PersonId)}={model.PersonId},{nameof(Registration.ScheduleId)}={model.ScheduleId},{nameof(Registration.CreateOn)}={model.CreateOn} WHERE {nameof(Registration.Id)}={model.Id}");
        }

        public Registration GetById(int id)
        {
            return Read($"SELECT  {nameof(Registration.Id)},{nameof(Registration.PersonId)}, {nameof(Registration.ScheduleId)},{nameof(Registration.CreateOn)},{nameof(Registration.Price)}  FROM {nameof(Registration)} WHERE {nameof(Registration.Id)}={id}");
        }

     

        public List<Registration> GetList()
        {
            var sqlCommand = $"SELECT  {nameof(Registration.Id)},{nameof(Registration.PersonId)}, {nameof(Registration.ScheduleId)},{nameof(Registration.CreateOn)},{nameof(Registration.Price)} FROM {nameof(Registration)}";
            return _dataBaseExecutor.Execute<List<Registration>>(connection =>
            {
                var command = new SqlCommand(sqlCommand, connection);
                var reader = command.ExecuteReader();
                if (!reader.HasRows) return null;
                List<Registration> _list = new List<Registration>();

                while (reader.Read())
                {
                    var Registration = _registrationFactory.Create(reader);
                    _list.Add(Registration);
                }

                return _list;
            });
        }

       
    }
}

