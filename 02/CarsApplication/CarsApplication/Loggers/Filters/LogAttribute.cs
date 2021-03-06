﻿namespace CarsApplication.Loggers.Filters
{
    using System;
    using System.Linq;

    using CarsApplication.Data;
    using CarsApplication.Data.Models;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Recommended usage with attribute [Authorize]
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class LogAttribute : ActionFilterAttribute
    {
        private string[] tablesToBeModified;
        
        public string OperationName { get; set; }

        public string[] TablesToBeModified {

            get
            {
                return this.tablesToBeModified == null ? new string[0] : this.tablesToBeModified;
            }
            set
            {
                this.tablesToBeModified = value == null ? new string[0] : value;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Canceled || context.Exception != null)
            {
                base.OnActionExecuted(context);
                return;
            }

            bool? isAuthenticated = context?.HttpContext?.User?.Identity.IsAuthenticated;
            if (isAuthenticated.HasValue && isAuthenticated.Value == true)
            {
                using (ApplicationDbContext dbContext = context.HttpContext.RequestServices.GetService<ApplicationDbContext>())
                {
                    ApplicationUser user = dbContext.Users.FirstOrDefault(u => u.UserName == context.HttpContext.User.Identity.Name);
                    if (user == null)
                    {
                        base.OnActionExecuted(context);
                        return;
                    }

                    Log log = new Log()
                    {
                        DateLogged = DateTime.UtcNow,
                        Message = $"Tables modified: {string.Join(", ", this.TablesToBeModified)}",
                        Name = this.OperationName ?? "Default"
                    };

                    user.Logs.Add(log);
                    dbContext.SaveChanges();
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
