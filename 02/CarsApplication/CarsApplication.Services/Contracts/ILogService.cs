namespace CarsApplication.Services.Contracts
{
    using System.Collections.Generic;

    using Models;

    public interface ILogService
    {
        IEnumerable<LogModel> Get(int page, int take);

        IEnumerable<LogModel> GetByUserName(string userName, int page, int take);

        int GetCountOfAll(string userName);

        int DeleteAll();
    }
}
