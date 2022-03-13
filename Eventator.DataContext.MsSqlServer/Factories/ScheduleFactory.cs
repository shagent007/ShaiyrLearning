using Eventator.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Eventator.DataContext.MsSqlServer.Factories
{
    public class ScheduleFactory:IFactory<Schedule>
    {
        public Schedule Create(SqlDataReader reader)
        {
            var _schedule = new Schedule();
            _schedule.Id = reader.GetInt32(nameof(_schedule.Id));
            _schedule.EventId = reader.GetInt32(nameof(_schedule.EventId));
            _schedule.StartDate = reader.GetDateTime(nameof(_schedule.StartDate));
            _schedule.MaxCapacity = reader.GetInt32(nameof(_schedule.MaxCapacity));
            _schedule.Price = reader.GetDecimal(nameof(_schedule.Price));
            return _schedule;
        }
    }
}

