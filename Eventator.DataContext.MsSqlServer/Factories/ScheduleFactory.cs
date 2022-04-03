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
            _schedule.Id = reader.GetInt32(nameof(Schedule.Id));
            _schedule.EventId = reader.GetInt32(nameof(Schedule.EventId));
            _schedule.StartDate = reader.GetDateTime(nameof(Schedule.StartDate));
            _schedule.MaxCapacity = reader.GetInt32(nameof(Schedule.MaxCapacity));
            _schedule.Price = reader.GetDecimal(nameof(Schedule.Price));
            return _schedule;
        }
    }
}

