namespace CarsApplication.Loggers.ApplicationDbLogger
{
    using CarsApplication.Data;
    using CarsApplication.Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using System;

    public class DatabaseLoggerProvider : ILoggerProvider
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IServiceProvider serviceProvider;

        public DatabaseLoggerProvider(ApplicationDbContext applicationDbContext, IServiceProvider serviceProvider)
        {
            this.dbContext = applicationDbContext;
            this.serviceProvider = serviceProvider;
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (categoryName != nameof(DatabaseLogger))
            {
                return NullLogger.Instance;
            }

            UserManager<ApplicationUser> userManager = this.serviceProvider.GetService<UserManager<ApplicationUser>>();
            IHttpContextAccessor accessor = this.serviceProvider.GetService<IHttpContextAccessor>();

            string userId = string.Empty;
            var userPrincipal = accessor?.HttpContext.User;
            if (userPrincipal != null && userPrincipal.Identity.IsAuthenticated)
            {
                userId = userManager.GetUserId(userPrincipal);
                // userId = userPrincipal.GetUserId();
            }

            return new DatabaseLogger(this.dbContext, categoryName, userId);
        }

        public void Dispose()
        {
        }
    }
}
