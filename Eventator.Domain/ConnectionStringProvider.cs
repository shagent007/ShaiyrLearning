namespace Eventator.Domain
{
    public class ConnectionStringProvider
    {
        private readonly string _connectionString;

        public ConnectionStringProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
        public string GetConnectionString()
        {
            return _connectionString;
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

