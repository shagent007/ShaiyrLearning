using Eventator.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Eventator.DataContext.MsSqlServer.Factories
{
    public class TicketFactory : IFactory<Ticket>
    {
        public Ticket Create(SqlDataReader reader)
        {
            var _ticket = new Ticket();
            _ticket.Id = reader.GetInt32(nameof(_ticket.Id));
            _ticket.PersonId = reader.GetInt32(nameof(_ticket.PersonId));
            _ticket.ScheduleId = reader.GetInt32(nameof(_ticket.ScheduleId));
            _ticket.CreateOn = reader.GetDateTime(nameof(_ticket.CreateOn));
            return _ticket;
        }
    }
}

