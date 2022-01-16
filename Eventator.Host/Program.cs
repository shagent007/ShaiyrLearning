using System.Configuration;
using Eventator.DataContext;
using Eventator.Domain.Models;

string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
DataBaseExecutor executor = new DataBaseExecutor(connectionString);
PersonRepository PersonRepository = new PersonRepository(executor);

/*


PersonRepository.Delete(3);
*/
Person person = new Person();
person.Name = "Шайырбек";
person.Email = "shagent118@gmail.com";
person.Age = 21;

PersonRepository.Add(person);


Console.WriteLine("Успешно выполнено");