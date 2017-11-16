namespace CarsApplication.Loggers.ApplicationDbLogger
{
    using CarsApplication.Data;
    using Microsoft.Extensions.Logging;

    public static class ServiceFactoryExtensions
    {
        public static ILoggerFactory AddDatabaseLogger(this ILoggerFactory factory, ApplicationDbContext dbContext)
        {
            factory.AddProvider(new DatabaseLoggerProvider(dbContext));
            return factory;
        }
    }
}
