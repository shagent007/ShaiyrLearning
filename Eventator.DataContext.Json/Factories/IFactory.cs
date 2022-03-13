using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventator.DataContext.Json.Factories
{
    public interface IFactory<Entity>
    {
        public Entity Create(string filePath);
    }
}
