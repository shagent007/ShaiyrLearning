using Eventator.Domain.Entities;
using Eventator.Domain.Wrappers;

namespace Eventator.FileManager
{
    public class FileManager
    {
        private readonly string _dirName;
        public readonly string _fileType;
        private readonly Serializer _serializer;
        public FileManager(string dirName, string fileType, Serializer serializer)
        {
            _dirName = dirName;
            _serializer = serializer;
            _fileType = fileType;
        }

        public void Add<EntityClass>(EntityClass entity) where EntityClass : Entity
        {
            var now = DateTime.Now;
            var zeroDate = DateTime.MinValue.AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddMilliseconds(now.Millisecond);
            entity.Id = (int)(zeroDate.Ticks / 10000);
            using (FileStream fs = new FileStream($"{_dirName}/{entity.Id}.{_fileType}", FileMode.Create))
            {
                _serializer.Serialize(fs, entity);
            } 
        }

        public void Update<EntityClass>(EntityClass entity) where EntityClass : Entity
        {
            var _filePath = $"{_dirName}/{entity.Id}.{_fileType}";
            if (!File.Exists(_filePath))
            {
                throw new DirectoryNotFoundException();
            }
            using (FileStream fs = new FileStream(_filePath, FileMode.Truncate))
            {
                _serializer.Serialize(fs, entity);
            }
           
        }

        public void Delete<EntityClass>(int enityId) where EntityClass : Entity
        {
            if (!File.Exists($"{_dirName}/{enityId}.{_fileType}"))
            {
                throw new FileNotFoundException();
            }
            File.Delete($"{_dirName}/{enityId}.{_fileType}");
        }

        public EntityClass GetEntity<EntityClass>(string fieldName, string fieldValue) where EntityClass : Entity
        {
            var _files = Directory.GetFiles(_dirName);
            if (_files.Length == 0)
                throw new FileNotFoundException();

            EntityClass? _entity = null;

            foreach (string filePath in _files)
            {
                using (FileStream fs = File.OpenRead(filePath))
                {
                    _entity = _serializer.Deserialize<EntityClass>(fs);
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
            var _files = Directory.GetFiles(_dirName);
            if (_files.Length == 0) return list;

            foreach (string filePath in _files)
            {
                EntityClass? _entity;
                using (FileStream fs = File.OpenRead(filePath))
                {
                    _entity = _serializer.Deserialize<EntityClass>(fs);
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