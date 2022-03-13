using Eventator.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Eventator.DataContext.MsSql.Factories
{
    public class RegistrationFactory
    {
        public Registration Create(SqlDataReader reader)
        {
            var _registration = new Registration();
            _registration.Id = reader.GetInt32(nameof(_registration.Id));
            _registration.PersonId = reader.GetInt32(nameof(_registration.PersonId));
            _registration.ScheduleId = reader.GetInt32(nameof(_registration.ScheduleId));
            _registration.CreateOn = reader.GetDateTime(nameof(_registration.CreateOn));
            _registration.Price = reader.GetInt32(nameof(_registration.Price));
            return _registration;
        }
    }
}

