namespace CarsApplication.Loggers.ApplicationDbLogger
{
    using CarsApplication.Data;
    using Microsoft.Extensions.Logging;

    public class DatabaseLoggerProvider : ILoggerProvider
    {
        private readonly ApplicationDbContext dbContext;

        public DatabaseLoggerProvider(ApplicationDbContext applicationDbContext)
        {
            this.dbContext = applicationDbContext;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(this.dbContext, categoryName);
        }

        public void Dispose()
        {
        }
    }
}
