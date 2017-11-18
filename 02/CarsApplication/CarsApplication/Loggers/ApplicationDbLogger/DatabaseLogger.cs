namespace CarsApplication.Loggers.ApplicationDbLogger
{
    using CarsApplication.Data;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Linq;

    public class DatabaseLogger : ILogger
    {
        private readonly ApplicationDbContext dbContext;
        private readonly string categoryName;
        private readonly string userId;

        public DatabaseLogger(ApplicationDbContext applicationDbContext, string category, string userId)
        {
            this.dbContext = applicationDbContext;
            this.categoryName = category;
            this.userId = userId;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabledCheck => this.categoryName == nameof(DatabaseLogger);

        public bool IsEnabled(LogLevel logLevel) => this.IsEnabledCheck;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabledCheck)
            {
                return;
            }

            string message = formatter(state, exception);

            bool userExists = this.dbContext.Users.Any(u => u.Id == this.userId);
            if (!userExists)
            {
                return;
            }

            this.dbContext.Logs.Add(new Data.Models.Log() { DateLogged = DateTime.UtcNow, Message = message, Name = $"{eventId.Name}: {eventId.Id}", UserId = this.userId });
            this.dbContext.SaveChanges();
        }
    }
}
