using Eventator.Domain;
using Eventator.Domain.Entities;
using Eventator.Domain.Exeptions;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Eventator.DataContext.Json
{
    public class JsonDataManager
    {
        private readonly string _personDirName;
        private readonly string _scheduleDirName;
        private readonly string _eventDirName;
        private readonly string _offerDirName;

        public JsonDataManager(ConnectionStringProvider provider)
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

        private string ConvertToJson<EntityClass>(EntityClass person) where EntityClass : Entity
        {
            return JsonSerializer.Serialize(person, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
            });
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

            string json = ConvertToJson<EntityClass>(entity);
            using (FileStream fs = new FileStream($"{_dirName}/{entity.Id}.json", FileMode.Create))
            {
                byte[] buffer = Encoding.Default.GetBytes(json);
                fs.Write(buffer, 0, buffer.Length);
            }
        }

        public void Update<EntityClass>(EntityClass entity) where EntityClass : Entity
        {
            var _dirName = GetEntityDirName(typeof(EntityClass).Name);
            var _filePath = $"{_dirName}/{entity.Id}.json";
            if (!File.Exists(_filePath))
            {
                throw new PersonNotFoundExeption();
            }
            string json = ConvertToJson<EntityClass>(entity);
            using (FileStream fs = new FileStream(_filePath, FileMode.Truncate))
            {
                byte[] buffer = Encoding.Default.GetBytes(json);
                fs.Write(buffer, 0, buffer.Length);
            }
        }

        public void Delete<EntityClass>(int enityId) where EntityClass : Entity
        {
            var _dirName = GetEntityDirName(typeof(EntityClass).Name);
            if (!File.Exists($"{_dirName}/{enityId}.json"))
            {
                throw new PersonNotFoundExeption();
            }
            File.Delete($"{_dirName}/{enityId}.json");
        }

        public EntityClass GetEntity<EntityClass>(string fieldName, string fieldValue) where EntityClass : Entity
        {
            var _dirName = GetEntityDirName(typeof(EntityClass).Name);
            var _files = Directory.GetFiles(_dirName);
            if (_files.Length == 0)
                throw new FileNotFoundException();

            EntityClass? _entity = null;
            foreach (string filePath in _files)
            {
                using (FileStream fs = File.OpenRead(filePath))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string json = Encoding.Default.GetString(buffer);
                    _entity = JsonSerializer.Deserialize<EntityClass>(json);
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
            foreach (string filePath in _files)
            {
                EntityClass? _entity;

                using (FileStream fs = File.OpenRead(filePath))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string json = Encoding.Default.GetString(buffer);
                    _entity = JsonSerializer.Deserialize<EntityClass>(json);
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
