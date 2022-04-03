using Eventator.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Eventator.DataContext.MsSqlServer
{
    public static class MsSqlServerExtensions
    {
        public static ServiceCollection BuildMsSqlStorage(this ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<MsSqlDataBaseExecutor>();
            serviceCollection.AddTransient<MsSqlDataManager>();
            serviceCollection.AddTransient<IPersonRepository, MsSqlPersonRepository>();
            serviceCollection.AddTransient<IScheduleRepository, MsSqlScheduleRepository>();
            serviceCollection.AddTransient<IEventRepository, MsSqlEventRepository>();
            return serviceCollection;
        }
    }
}