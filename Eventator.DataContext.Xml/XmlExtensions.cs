using Eventator.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Eventator.DataContext.Xml
{
    public static class XmlExtensions
    {
        public static ServiceCollection BuildXmlStorage(this ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<XmlDataManager>();
            serviceCollection.AddTransient<IPersonRepository, XmlPersonRepository>();
            serviceCollection.AddTransient<IScheduleRepository, XmlScheduleRepository>();
            serviceCollection.AddTransient<IEventRepository, XmlEventRepository>();
            return serviceCollection;
        }
    }
}