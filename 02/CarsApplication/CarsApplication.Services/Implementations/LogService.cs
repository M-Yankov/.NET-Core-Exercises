namespace CarsApplication.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;

    using CarsApplication.Services.Models;
    using Contracts;
    using Data;

    public class LogService : ILogService
    {
        private readonly ApplicationDbContext dbContext;

        public LogService(ApplicationDbContext applicationDbContext)
        {
            this.dbContext = applicationDbContext;
        }

        public int DeleteAll()
        {
            this.dbContext.Logs.RemoveRange(this.dbContext.Logs);
            int deletedRows = this.dbContext.SaveChanges();
            return deletedRows;
        }

        public IEnumerable<LogModel> Get(int page, int take)
        {
            page = page <= 0 ? 1 : page;
            take = take <= 0 ? 10 : take;

            return this.dbContext.Logs
                .OrderByDescending(l => l.DateLogged)
                .Skip((page - 1) * take)
                .Take(take)
                .Select(l => new LogModel()
                {
                    DateLogged = l.DateLogged,
                    OperationName = l.Name,
                    TablesModified = l.Message,
                    UserName = l.User.UserName
                })
                .ToList();
        }

        public IEnumerable<LogModel> GetByUserName(string userName, int page, int take)
        {
            page = page <= 0 ? 1 : page;
            take = take <= 0 ? 10 : take;
            userName = string.IsNullOrEmpty(userName) ? string.Empty : userName;

            userName = userName.Trim();

            return this.dbContext.Logs
                .Where(l => l.User.UserName.ToLower() == userName.ToLower())
                .OrderByDescending(l => l.DateLogged)
                .Skip((page - 1) * take)
                .Take(take)
                .Select(l => new LogModel()
                {
                    DateLogged = l.DateLogged,
                    OperationName = l.Name,
                    TablesModified = l.Message,
                    UserName = l.User.UserName
                })
                .ToList();
        }

        public int GetCountOfAll(string userName) 
            => 
            this.dbContext.Logs
            .Where(l => string.IsNullOrEmpty(userName) || l.User.UserName.ToLower() == userName.ToLower() )
            .Count();
    }
}
