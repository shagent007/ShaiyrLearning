using Eventator.DataContext.MsSqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventator.Host
{
    public class Eventator
    {
        private readonly MsSqlDataManager _dataManager;

        public Eventator(string connectionString)
        {
            var dataBaseExecutor = new MsSqlDataBaseExecutor(connectionString);
            _dataManager = new MsSqlDataManager(dataBaseExecutor);
        }
        public void Start()
        {
            List<string> _commands = new List<string>()
            {
                "Войти",
                "Выйти",
            };
            string _command;
            do
            {
                _command = Ask.ItemInStringList("Что вы хотите сделать (Войти/Выйти): ", _commands);
                switch (_command)
                {
                    case "Войти":
                        ChooseRole();
                        break;
                }
            } while (_command != "Выйти");
        }

        public void ChooseRole()
        {
            List<string> _commands = new List<string>()
            {
                "Гость",
                "Админ",
                "Назад"
            };
            string _command;
            do
            {
                _command = Ask.ItemInStringList("Введите вашу роль (Гость/Админ/Назад): ", _commands);
                switch (_command)
                {
                    case "Гость":
                        ChooseEvent();
                        break;
                    case "Админ":

                        break;
                }
            } while (_command != "Назад");
        }

        public void ChooseEvent()
        {
            var _rep = new MsSqlEventRepository(_dataManager);
            var _events = _rep.GetList();

            Console.WriteLine("Список событий");
            foreach (var _event in _events)
            {
                Console.WriteLine(@$"{_event.Id}, {_event.Name}");
            }

            var _eventIdList = _events
                .Select(e => e.Id.ToString())
                .ToList();

            _eventIdList.Add("Назад");

            string _eventId;
            do
            {
                _eventId = Ask.ItemInStringList("Выберите id нужного событие:(Чтобы вернуться введите 'Назад') ", _eventIdList);
                if (_eventId != "Назад")
                {
                    ChooseSchedule(int.Parse(_eventId));
                }
            } while (_eventId != "Назад");
        }

        public void ChooseSchedule(int eventId)
        {
            var _rep = new MsSqlScheduleRepository(_dataManager);
            var _schedules = _rep.GetListByEventId(eventId);

            if(_schedules.Count() == 0)
            {
                Console.WriteLine("Для данного событие ещё нету сеанса. Пожалуйста выберите другой сеанс.");
                return;
            }
           

            Console.WriteLine("Список доступных сеансов");
            foreach (var _schedule in _schedules)
            {
                Console.WriteLine(@$"{_schedule.Id}, {_schedule.StartDate.ToShortDateString()}");
            }
            var _scheduleIdList = _schedules
              .Select(s => s.Id.ToString())
              .ToList();

            _scheduleIdList.Add("Назад");

            string _scheduleId;
            do
            {
                _scheduleId = Ask.ItemInStringList("Выберите id нужного сеанса:(Чтобы вернуться введите 'Назад') ", _scheduleIdList);
                if (_scheduleId != "Назад")
                {
                    BuyTicket(int.Parse(_scheduleId));
                }
            } while (_scheduleId != "Назад");
        }


        public void BuyTicket(int scheduleId)
        {
            var _scheduleRep = new MsSqlScheduleRepository(_dataManager);
            var _schedule = _scheduleRep.GetById(scheduleId);
            if(_schedule.Tickets.Count() >= _schedule.MaxCapacity)
            {
                Console.WriteLine("Для данного сеанса не осталось билетов. Выберите другой сеанс.");
                return;
            }
            int _ticketCount = _schedule.MaxCapacity - _schedule.Tickets.Count();
            Console.WriteLine($"Для данного сеанса осталось {_ticketCount} билетов");
            int _toBuyTicketCount;
            do
            {
                _toBuyTicketCount = Ask.Int("Введите количество билетов которое вы хотите купить: ");
                if(_toBuyTicketCount > _ticketCount)
                {
                    Console.WriteLine($"У нас нету столько билетов)) Осталось только {_ticketCount}") ;
                }

            } while (_toBuyTicketCount > _ticketCount);

            Console.WriteLine("Успех");
        }
    }
}
