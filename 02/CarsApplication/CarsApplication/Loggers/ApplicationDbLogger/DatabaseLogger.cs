namespace CarsApplication.Loggers.ApplicationDbLogger
{
    using CarsApplication.Data;
    using Microsoft.Extensions.Logging;
    using System;

    public class DatabaseLogger : ILogger
    {
        private readonly ApplicationDbContext dbContext;
        private readonly string categoryName;

        public DatabaseLogger(ApplicationDbContext applicationDbContext, string category)
        {
            this.dbContext = applicationDbContext;
            this.categoryName = category;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel) => this.categoryName == nameof(DatabaseLogger);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = formatter(state, exception);

            if (this.categoryName != nameof(DatabaseLogger))
            {
                return;
            }

            this.dbContext.Logs.Add(new Data.Models.Log() { DateLogged = DateTime.UtcNow, Message = message, Name = $"{eventId.Name}: {eventId.Id}" });
            this.dbContext.SaveChanges();


        }
    }
}
