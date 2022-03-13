using Eventator.Domain.Entities;
using Eventator.Domain.Exeptions;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Eventator.DataContext.Xml.Factories
{
    public class PersonFactory : IFactory<Person>
    {
        public Person Create(string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));
            using (FileStream fs = File.OpenRead(filePath))
            {
                try
                {
                    Person? person = xmlSerializer.Deserialize(fs) as Person;
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
