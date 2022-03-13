using Eventator.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Eventator.DataContext.MsSql.Factories
{
    public class EventFactory
    {

        public Event Create(SqlDataReader reader)
        {
            var _event = new Event();
            _event.Id = reader.GetInt32(nameof(_event.Id));
            _event.Description = reader.GetString(nameof(_event.Description));
            _event.Name = reader.GetString(nameof(_event.Name));
            return _event;
        }

    }
}

