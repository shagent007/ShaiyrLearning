using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventator.Domain.Repositories
{
    public interface IRepository<Model>
    {
        Model Get(int id);
        void Add(Model model);
        void Update(Model model);
        void Delete(int id);
    }
}
