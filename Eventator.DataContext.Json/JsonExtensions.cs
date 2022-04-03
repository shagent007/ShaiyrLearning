using Eventator.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Eventator.DataContext.Json
{
    public static class JsonExtensions
    {
        public static ServiceCollection BuildJsonStorage(this ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<JsonDataManager>();
            serviceCollection.AddTransient<IPersonRepository, JsonPersonRepository>();
            serviceCollection.AddTransient<IScheduleRepository, JsonScheduleRepository>();
            serviceCollection.AddTransient<IEventRepository, JsonEventRepository>();
            return serviceCollection;
        }
    }
}
