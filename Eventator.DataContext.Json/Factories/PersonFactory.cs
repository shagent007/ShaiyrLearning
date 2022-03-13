using Eventator.Domain.Entities;
using Eventator.Domain.Exeptions;
using System.Text;
using System.Text.Json;

namespace Eventator.DataContext.Json.Factories
{
    public class PersonFactory : IFactory<Person>
    {
        public Person Create(string filePath)
        {
            using (FileStream fstream = File.OpenRead(filePath))
            {
                byte[] buffer = new byte[fstream.Length];
                fstream.Read(buffer, 0, buffer.Length);
                string json = Encoding.Default.GetString(buffer);

                try
                {
                    Person? person = JsonSerializer.Deserialize<Person>(json);
                    if (person == null)
                        throw new InvalidPersonExeption();

                    return person;
                }
                catch (Exception)
                {
                    throw new InvalidPersonExeption();
                }
            }
        }
    }
}
