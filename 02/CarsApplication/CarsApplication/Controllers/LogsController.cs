using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarsApplication.Controllers
{
    using CarsApplication.Models.Logs;
    using Services.Contracts;

    public class LogsController : Controller
    {
        private const int TakeCount = 2;

        private readonly ILogService logService;

        public LogsController(ILogService logService)
        {
            this.logService = logService;
        }

        [HttpGet]
        public IActionResult All(string userName, int page = 1)
        {
            LogViewModel model = new LogViewModel();
            if (string.IsNullOrWhiteSpace(userName))
            {
                model.Items = this.logService.Get(page, TakeCount);
            }
            else
            {
                model.Items = this.logService.GetByUserName(userName, page, TakeCount);
            }

            int allLogs = this.logService.GetCountOfAll(userName);
            model.Allpages = (int)Math.Ceiling(Convert.ToDouble(allLogs) / Convert.ToDouble(TakeCount));
            model.CurrentPage = page;
            model.Username = userName;

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Delete()
        {
            int logsDeleted = this.logService.DeleteAll();

            this.TempData["Message"] = $"Deleted: {logsDeleted}";
            return this.RedirectToAction(nameof(All));
        }
    }
}
