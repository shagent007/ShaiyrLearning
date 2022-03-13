using Eventator.DataContext.Xml.Factories;
using Eventator.Domain.Entities;
using Eventator.Domain.Exeptions;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Xml.Serialization;

namespace Eventator.DataContext.Xml
{
    public class XmlDataManager
    {
        private readonly string _personDirName;
        private readonly string _scheduleDirName;
        private readonly string _eventDirName;
        private readonly string _offerDirName;

        public XmlDataManager(string rootDirName)
        {
            _personDirName = $@"{rootDirName}\Persons";
            _scheduleDirName = $@"{rootDirName}\Schedules";
            _eventDirName = $@"{rootDirName}\Events";
            _offerDirName = $@"{rootDirName}\Offers";

            var _directories = new List<string>()
            {
                _personDirName,
                _scheduleDirName,
                _eventDirName,
                _offerDirName
            };

            foreach (var directory in _directories)
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
        }

        private string GetEntityDirName(string entityName) => entityName switch
        {
            nameof(Person) => _personDirName,
            nameof(Schedule) => _scheduleDirName,
            nameof(Event) => _eventDirName,
            nameof(Offer) => _offerDirName
        };
        

        public void Add<EntityClass>(EntityClass entity) where EntityClass : Entity
        {
            var now = DateTime.Now;
            var _dirName = GetEntityDirName(typeof(EntityClass).Name);
            var zeroDate = DateTime.MinValue.AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddMilliseconds(now.Millisecond);
            entity.Id = (int)(zeroDate.Ticks / 10000);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(EntityClass));
           
            using (FileStream fs = new FileStream($"{_dirName}/{entity.Id}.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, entity);
            }
        }

        public void Update<EntityClass>(EntityClass entity) where EntityClass : Entity
        {
            var _dirName = GetEntityDirName(typeof(EntityClass).Name);
            var _filePath = $"{_dirName}/{entity.Id}.xml";
            if (!File.Exists(_filePath))
            {
                throw new PersonNotFoundExeption();
            }
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(EntityClass));
            using (FileStream fs = new FileStream(_filePath, FileMode.Truncate))
            {
                xmlSerializer.Serialize(fs, entity);
            }
        }

        public void Delete<EntityClass>(int enityId) where EntityClass : Entity
        {
            var _dirName = GetEntityDirName(typeof(EntityClass).Name);
            if (!File.Exists($"{_dirName}/{enityId}.xml"))
            {
                throw new PersonNotFoundExeption();
            }
            File.Delete($"{_dirName}/{enityId}.xml");
        }

        public Person GetPerson(string fieldName, string fieldValue)
        {
            var _factory = new PersonFactory();
            var _files  = Directory.GetFiles(_personDirName);
            if (_files.Length == 0)
                throw new PersonNotFoundExeption();
            Person? _person = null;
            foreach (string filePath in _files)
            {
                try
                {
                    Person person = _factory.Create(filePath);
                    string? value = person?.GetType()
                        ?.GetProperty(fieldName)
                        ?.GetValue(person, null)?.ToString();
                    if (value == fieldValue)
                    {
                        _person = person;
                    }
                }
                catch (InvalidPersonExeption)
                {
                    continue;
                }
            }
            if (_person == null)
            {
                throw new PersonNotFoundExeption();
            }

            return _person;
        }

        public List<Person> GetPeople(string? fieldName=null, string? fieldValue=null)
        {
            var _factory = new PersonFactory();
            var list = new List<Person>();
            var _files = Directory.GetFiles(_personDirName);
            if (_files.Length == 0) return list;

            foreach (string filePath in _files)
            {
                try
                {
                    Person person = _factory.Create(filePath);

                    if (fieldName != null && fieldValue != null)
                    {
                        var value = person.GetType()
                            ?.GetProperty(fieldName)
                            ?.GetValue(person, null)?.ToString();
                        if (value == fieldValue)
                        {
                           list.Add(person);
                        }
                    } else
                    {
                        list.Add(person);
                    }
                }
                catch (InvalidPersonExeption)
                {
                    continue;
                }
            }

            return list;
        }


    }
}
