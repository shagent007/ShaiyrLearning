using Eventator.DataContext.MsSqlServer.Factories;
using Eventator.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Eventator.DataContext.MsSqlServer
{
    public class MsSqlDataManager
    {
        private readonly MsSqlDataBaseExecutor _executor;
        public MsSqlDataManager(MsSqlDataBaseExecutor executor)
        {
            _executor = executor;
        }

        public void Write(string sqlCommand)
        {
            _executor.Write(sqlCommand);
        }

        public Person GetPersonByField(string fieldName, string fieldValue)
        {
            var _factory = new PersonFactory();
            var sqlCommand = @$"
                SELECT
                    {nameof(Person.Id)},
                    {nameof(Person.Name)}, 
                    {nameof(Person.Age)}, 
                    {nameof(Person.Email)} 
                FROM 
                    {nameof(Person)} 
                WHERE 
                    {fieldName}='{fieldValue}'
            ";
            return _executor.ReadItem(sqlCommand, dataReader => _factory.Create(dataReader));
        }

        public List<Person> GetPersonList()
        {
            var _factory = new PersonFactory();
            var sqlCommand = @$"
                SELECT 
                    {nameof(Person.Id)},
                    {nameof(Person.Name)}, 
                    {nameof(Person.Age)}, 
                    {nameof(Person.Email)} 
                FROM 
                    {nameof(Person)}
            ";
            return _executor.ReadList(sqlCommand, dataReader => _factory.Create(dataReader));
        }

        // event
        public Event GetEventByField(string fieldName, string fieldValue)
        {
            var _factory = new EventFactory();
            var sqlCommand = @$"
                SELECT
                    {nameof(Event.Id)},
                    {nameof(Event.Name)}, 
                    {nameof(Event.Description)}
                FROM 
                    {nameof(Event)} 
                WHERE 
                    {fieldName}='{fieldValue}'
            ";
            return _executor.ReadItem(sqlCommand, dataReader => _factory.Create(dataReader));
        }

        public List<Event> GetEventList()
        {
            var _factory = new EventFactory();
            var sqlCommand = @$"
                SELECT 
                    {nameof(Event.Id)},
                    {nameof(Event.Name)}, 
                    {nameof(Event.Description)} 
                FROM 
                    {nameof(Event)}
            ";
            return _executor.ReadList(sqlCommand, dataReader => _factory.Create(dataReader));
        }

        // schedule
        public Schedule GetScheduleByField(string fieldName, string fieldValue)
        {
            var _factory = new ScheduleFactory();
            var sqlCommand = @$"
                SELECT
                    {nameof(Schedule.Id)},
                    {nameof(Schedule.EventId)}, 
                    {nameof(Schedule.StartDate)}
                FROM 
                    {nameof(Schedule)} 
                WHERE 
                    {fieldName}='{fieldValue}'
            ";
            return _executor.ReadItem(sqlCommand, dataReader => _factory.Create(dataReader));
        }

        public List<Schedule> GetScheduleListByField(string fieldName, string fieldValue)
        {
            var _factory = new ScheduleFactory();
            var sqlCommand = @$"
                SELECT 
                    {nameof(Schedule.Id)},
                    {nameof(Schedule.EventId)}, 
                    {nameof(Schedule.StartDate)},
                    {nameof(Schedule.Price)},
                    {nameof(Schedule.MaxCapacity)}
                FROM 
                    {nameof(Schedule)}
                WHERE 
                    {fieldName}='{fieldValue}'
            ";
            return _executor.ReadList(sqlCommand, dataReader => _factory.Create(dataReader));
        }

        public List<Schedule> GetScheduleList()
        {
            var _factory = new ScheduleFactory();
            var sqlCommand = @$"
                SELECT 
                    {nameof(Schedule.Id)},
                    {nameof(Schedule.EventId)}, 
                    {nameof(Schedule.StartDate)},
                    {nameof(Schedule.Price)},
                    {nameof(Schedule.MaxCapacity)}
                FROM 
                    {nameof(Schedule)}
            ";
            return _executor.ReadList(sqlCommand, dataReader => _factory.Create(dataReader));
        }

        public List<Schedule> GetSchedulesByField(string fieldName, string fieldValue)
        {
            var _factory = new ScheduleFactory();
            var sqlCommand = @$"
                SELECT
                    {nameof(Schedule.Id)},
                    {nameof(Schedule.EventId)}, 
                    {nameof(Schedule.StartDate)},
                    {nameof(Schedule.Price)},
                    {nameof(Schedule.MaxCapacity)}
                FROM 
                    {nameof(Schedule)}
                WHERE 
                    {fieldName}='{fieldValue}'
            ";
            return _executor.ReadList(sqlCommand, dataReader => _factory.Create(dataReader));
        }

        // ticket
        public Ticket GetTicketByField(string fieldName, string fieldValue)
        {
            var _factory = new TicketFactory();
            var sqlCommand = @$"
                SELECT
                    {nameof(Ticket.Id)},
                    PersonId, 
                    ScheduleId,
                    {nameof(Ticket.CreateOn)}
                FROM 
                    {nameof(Ticket)} 
                WHERE 
                    {fieldName}='{fieldValue}'
            ";
            return _executor.ReadItem(sqlCommand, dataReader => _factory.Create(dataReader));
        }

        public List<Ticket> GetTicketsByField(string fieldName, string fieldValue)
        {
            var _factory = new TicketFactory();
            var sqlCommand = @$"
                SELECT
                    {nameof(Ticket.Id)},
                    {nameof(Ticket.PersonId)}, 
                    {nameof(Ticket.ScheduleId)},
                    {nameof(Ticket.CreateOn)}
                FROM 
                    {nameof(Ticket)} 
                WHERE 
                    {fieldName}='{fieldValue}'
            ";
            return _executor.ReadList(sqlCommand, dataReader => _factory.Create(dataReader));
        }

        public List<Ticket> GetTicketList()
        {
            var _factory = new TicketFactory();
            var sqlCommand = @$"
                SELECT
                    {nameof(Ticket.Id)},
                    {nameof(Ticket.PersonId)}, 
                    {nameof(Ticket.ScheduleId)},
                    {nameof(Ticket.CreateOn)}
                FROM 
                    {nameof(Ticket)}
            ";
            return _executor.ReadList(sqlCommand, dataReader => _factory.Create(dataReader));
        }
    }
}