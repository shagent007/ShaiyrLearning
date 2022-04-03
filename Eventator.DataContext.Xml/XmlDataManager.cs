using Eventator.Domain;
using Eventator.Domain.Entities;
using Eventator.Domain.Exeptions;
using System.Xml.Serialization;

namespace Eventator.DataContext.Xml
{
    public class XmlDataManager
    {
        private readonly string _personDirName;
        private readonly string _scheduleDirName;
        private readonly string _eventDirName;
        private readonly string _offerDirName;

        public XmlDataManager(ConnectionStringProvider provider)
        {
            var _rootDirName = provider.GetConnectionString();
            _personDirName = $@"{_rootDirName}\Persons";
            _scheduleDirName = $@"{_rootDirName}\Schedules";
            _eventDirName = $@"{_rootDirName}\Events";
            _offerDirName = $@"{_rootDirName}\Offers";

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
            nameof(Offer) => _offerDirName,
            _ => throw new NotFoundExeption()
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

        public EntityClass GetEntity<EntityClass>(string fieldName, string fieldValue) where EntityClass : Entity
        {
            var _dirName = GetEntityDirName(typeof(EntityClass).Name);
            var _files = Directory.GetFiles(_dirName);
            if (_files.Length == 0)
                throw new FileNotFoundException();

            EntityClass? _entity = null;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(EntityClass));
            foreach (string filePath in _files)
            {
                using (FileStream fs = File.OpenRead(filePath))
                {
                    _entity = xmlSerializer.Deserialize(fs) as EntityClass;
                }

                string? value = _entity?.GetType()
                   ?.GetProperty(fieldName)
                   ?.GetValue(_entity, null)?.ToString();

                if (_entity != null && value == fieldValue)
                {
                    return _entity;
                }
            }

            throw new FileNotFoundException();
        }

        public List<EntityClass> GetEntities<EntityClass>(string? fieldName = null, string? fieldValue = null) where EntityClass : Entity
        {
            var list = new List<EntityClass>();
            var _dirName = GetEntityDirName(typeof(EntityClass).Name);
            var _files = Directory.GetFiles(_dirName);
            if (_files.Length == 0) return list;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(EntityClass));
            foreach (string filePath in _files)
            {
                EntityClass? _entity;
               
                using (FileStream fs = File.OpenRead(filePath))
                {
                    _entity = xmlSerializer.Deserialize(fs) as EntityClass;
                }

                if (_entity == null) continue;

                if (fieldName != null && fieldValue != null)
                {
                    var value = _entity.GetType()
                        ?.GetProperty(fieldName)
                        ?.GetValue(_entity, null)?.ToString();
                    if (value == fieldValue)
                    {
                        list.Add(_entity);
                    }
                }
                else
                {
                    list.Add(_entity);
                }


            }

            return list;
        }

    }
}
