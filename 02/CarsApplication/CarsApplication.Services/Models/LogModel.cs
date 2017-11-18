namespace CarsApplication.Services.Models
{
    using System;

    public class LogModel
    {
        public string UserName { get; set; }

        public DateTime DateLogged { get; set; }

        public string OperationName { get; set; }

        public string TablesModified { get; set; }
    }
}
