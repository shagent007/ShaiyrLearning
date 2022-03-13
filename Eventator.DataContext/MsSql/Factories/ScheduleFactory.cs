using Eventator.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Eventator.DataContext.MsSql.Factories
{
    public class ScheduleFactory
    {
        public Schedule Create(SqlDataReader reader)
        {
            var _schedule = new Schedule();
            _schedule.Id = reader.GetInt32(nameof(_schedule.Id));
            _schedule.EventId = reader.GetInt32(nameof(_schedule.EventId));
            _schedule.StartDate = reader.GetDateTime(nameof(_schedule.StartDate));
            return _schedule;
        }
    }
}

