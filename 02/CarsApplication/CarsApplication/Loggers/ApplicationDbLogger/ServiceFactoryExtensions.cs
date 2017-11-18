namespace CarsApplication.Loggers.ApplicationDbLogger
{
    using CarsApplication.Data;
    using Microsoft.Extensions.Logging;

    public static class ServiceFactoryExtensions
    {
        public static ILoggerFactory AddDatabaseLogger(this ILoggerFactory factory, ApplicationDbContext dbContext, System.IServiceProvider serviceProvider)
        {
            factory.AddProvider(new DatabaseLoggerProvider(dbContext, serviceProvider));
            return factory;
        }
    }
}
