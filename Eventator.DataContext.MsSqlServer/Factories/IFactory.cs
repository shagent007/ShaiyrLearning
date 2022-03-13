using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventator.DataContext.MsSqlServer.Factories
{
    internal interface IFactory<Entity>
    {
        public Entity Create(SqlDataReader reader);
    }
}
