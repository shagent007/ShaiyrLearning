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

        public void Delete<BaseClass>(int id) where BaseClass : Entity
        {
            _executor.Update(@$"DELETE {typeof(BaseClass).Name} WHERE Id={id}");
        }

        public Person GetPerson(string fieldName, string fieldValue)
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

        public List<Person> GetPersons()
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

        public void UpdatePerson(Person person)
        {
            int affectedRows = _executor.Update(@$"
                UPDATE 
                    {nameof(Person)} 
                SET 
                    {nameof(Person.Age)}={person.Age},
                    {nameof(Person.Name)}='{person.Name}',
                    {nameof(Person.Email)}='{person.Email}'
                WHERE 
                    {nameof(Person.Id)}={person.Id}
            ");

            if (affectedRows == 0)
            {
                throw new Exception("Не удалось обновить возможно человек не сушествует");
            }
        }

        public void AddPerson(Person person)
        {
            person.Id = _executor.Insert<int>(@$"
                INSERT INTO {nameof(Person)} 
                    (
                        {nameof(Person.Age)},
                        {nameof(Person.Name)},
                        {nameof(Person.Email)}
                    )
                VALUES 
                    (
                        {person.Age},
                        '{person.Name}',
                        '{person.Email}'
                    )
                    
            ");

            if (person.IsPersisted)
            {
                throw new Exception("Не удалось добавить");
            }
        }

        // event
        public Event GetEvent(string fieldName, string fieldValue)
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
        public List<Event> GetEvents()
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
        public void AddEvent(Event model)
        {
            model.Id = _executor.Insert<int>(@$"
                INSERT INTO {nameof(Event)} 
                    (
                        {nameof(Event.Name)}, 
                        {nameof(Event.Description)}
                    ) 
                VALUES 
                    (
                        '{model.Name}', 
                        '{model.Description}'
                    )
            ");

            if (!model.IsPersisted)
            {
                throw new Exception("Не удалось добавить");
            }
        }
        public void UpdateEvent(Event model)
        {
            int affectedRows = _executor.Update(@$"
                UPDATE 
                    {nameof(Event)} 
                SET 
                    {nameof(Event.Description)}='{model.Description}',
                    {nameof(Event.Name)}='{model.Name}' 
                WHERE 
                    {nameof(Event.Id)}={model.Id}
            ");

            if (affectedRows == 0)
            {
                throw new Exception("Не удалось обновить");
            }
        }


        // schedule
        public Schedule GetSchedule(string fieldName, string fieldValue)
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
            return _executor.ReadItem(sqlCommand, dataReader => _factory.Create(dataReader));
        }
        public List<Schedule> GetSchedules(string? fieldName = null, string? fieldValue = null)
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
            if(fieldName != null && fieldValue != null)
            {
                sqlCommand += $"WHERE {fieldName}='{fieldValue}'";
            }

            return _executor.ReadList(sqlCommand, dataReader => _factory.Create(dataReader));
        }
        public void UpdateSchedule(Schedule schedule)
        {
            int affectedRows = _executor.Update(@$"
                UPDATE 
                    {nameof(Schedule)} 
                SET 
                    {nameof(Schedule.EventId)}={schedule.EventId},
                    {nameof(Schedule.StartDate)}={schedule.StartDate},
                    {nameof(Schedule.Price)}={schedule.Price},
                    {nameof(Schedule.MaxCapacity)}={schedule.MaxCapacity}
                WHERE 
                    {nameof(Schedule.Id)}={schedule.Id}
            ");

            if (affectedRows == 0)
            {
                throw new Exception("Не удалось обновить");
            }
        }
        public void AddSchedule(Schedule schedule)
        {
            schedule.Id = _executor.Insert<int>(@$"
                INSERT INTO {nameof(Schedule)}
                    (
                        {nameof(Schedule.EventId)},
                        {nameof(Schedule.StartDate)},
                        {nameof(Schedule.Price)},
                        {nameof(Schedule.MaxCapacity)}
                    )
                VALUES
                    (
                        {schedule.EventId},
                        '{schedule.StartDate}',
                        {schedule.Price},
                        {schedule.MaxCapacity}
                    )
            ");

            if (schedule.IsPersisted)
            {
                throw new Exception("Не удалось Добавить");
            }
        }


        // ticket
        public Ticket GetTicket(string fieldName, string fieldValue)
        {
            var _factory = new TicketFactory();
            var sqlCommand = @$"
                SELECT
                    {nameof(Ticket.Id)},
                    {nameof(Ticket.PersonId)}, 
                    {nameof(Ticket.ScheduleId)},
                    {nameof(Ticket.CreateOn)},
                FROM 
                    {nameof(Ticket)} 
                WHERE 
                    {fieldName}='{fieldValue}'
            ";
            return _executor.ReadItem(sqlCommand, dataReader => _factory.Create(dataReader));
        }
        public List<Ticket> GetTickets(string? fieldName = null, string? fieldValue = null)
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

            if (fieldName != null && fieldValue != null)
            {
                sqlCommand += $"WHERE {fieldName} = '{fieldValue}'";
            }

            return _executor.ReadList(sqlCommand, dataReader => _factory.Create(dataReader));
        }
        public void UpdateTicket(Ticket ticket)
        {
            int affectedRows = _executor.Update(@$"
                UPDATE 
                    {nameof(Ticket)} 
                SET 
                    {nameof(Ticket.ScheduleId)}={ticket.ScheduleId},
                    {nameof(Ticket.PersonId)}={ticket.PersonId},
                    {nameof(Ticket.CreateOn)}='{ticket.CreateOn}'
                WHERE 
                    {nameof(Ticket.Id)}={ticket.Id}
            ");

            if (affectedRows == 0)
            {
                throw new Exception("Не удалось обновить возможно данный билет не сушествует");
            }
        }
        public void AddTicket(Ticket ticket)
        {
            ticket.Id = _executor.Insert<int>(@$"
                INSERT INTO {nameof(Ticket)} 
                    (
                        {nameof(Ticket.ScheduleId)},
                        {nameof(Ticket.PersonId)},
                        {nameof(Ticket.CreateOn)}
                    ) 
                VALUES
                    (
                        {ticket.ScheduleId},
                        {ticket.PersonId},
                        '{ticket.CreateOn}'
                    )
            ");

            if (!ticket.IsPersisted)
            {
                throw new Exception("Не удалось добавить");
            }
        }
    }
}