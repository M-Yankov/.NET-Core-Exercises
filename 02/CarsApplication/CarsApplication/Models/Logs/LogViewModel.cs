namespace CarsApplication.Models.Logs
{
    using System.Collections.Generic;
    using Services.Models;

    public class LogViewModel
    {
        public int Allpages { get; set; }

        public int CurrentPage { get; set; }

        public string Username { get; set; }

        public IEnumerable<LogModel> Items { get; set; }
    }
}
