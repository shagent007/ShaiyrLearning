
using Eventator.Domain.Entities;
using Eventator.Domain.Services;
using Eventator.DataContext.MsSqlServer;
using Eventator.DataContext.Json;
using Eventator.Domain.Exeptions;
using Eventator.DataContext.Xml;

namespace Eventator.Host
{
    partial class Program
    {
        static void Main(string[] args)
        {
            //  Eventator eventator = new Eventator("Data Source = 195.133.144.133, 49158; Database = Eventator_SHAIR; User ID = Shair; Password = 1234; TrustServerCertificate = true");
            //  eventator.Start();
            var _rootDir = @"D:\Документы\учёба\Eventator\Eventator.DataContext.Xml\Sources";
            var _manager = new XmlDataManager(_rootDir);
            var _rep = new XmlPersonPerository(_manager);
           /*  var _person = new Person()
             {
                 Name = "Аман",
                 Age = 24,
                 Email = "ilyz@gmail.com"
             };
             _rep.Add(_person);

            var people = _rep.GetList();
            people.ForEach(p => Console.WriteLine($@"{p.Id}, {p.Name}, {p.Age}, {p.Email}"));*/

            try
            {
              /* var _person = _rep.GetById(34810835);
                Console.WriteLine($@"{_person.Id}, {_person.Name}, {_person.Age}, {_person.Email}");
                 var _person = _rep.GetById(34973614);
                Console.WriteLine($@"{_person.Id}, {_person.Name}, {_person.Age}, {_person.Email}");
                _person.Name = "Лев лещенко";
                _person.Age = 78;
                _person.Email = "lev@gmail.com";
                _rep.Update(_person);

                var _person = _rep.GetByEmail("lev@gmail.com");
                Console.WriteLine($@"{_person.Id}, {_person.Name}, {_person.Age}, {_person.Email}");*/
                _rep.Delete(46629004);
            }
            catch (PersonNotFoundExeption e)
            {
                Console.WriteLine("Человек не найден");
            }
            catch (InvalidPersonExeption e)
            {
                Console.WriteLine("Человек не правелен");
            }
           
        }
    }
}


// 1. Выберите событие
// Вывод всех событий 
// Пользовотель вводит id нужного Event a

// 2. Выберите сеанс
// Вывод всех сеансов выбранного Event a
// Пользователь вводит id нужного Schedule

// 3. Проверить возможность продажи билета на нужный schedule
// Если билеты есть показываем сообшение о том что билетов нету и предлагаем выбрать новый сеанс
// Если билет есть то - 4

// 4. Введите данные о покупателе а так же количество мест
// Проверить перед продажей количество билет 
// Если доступные билеты меньще чем количество мест то выдаем ошибку и прделагаем написать новую количеству
// Если количество билетов достаточно то регистрируем клиента и выпускаем билет

