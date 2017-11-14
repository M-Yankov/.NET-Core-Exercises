namespace CarsApplication.Services.Models
{
    using System;

    public class CustomerDetails
    {
        public string Name { get; set; }

        public DateTime DateOfBith { get; set; }

        public int CarsBought { get; set; }

        public decimal TotalSpentMoney { get; set; }

        public int Id { get; set; }
    }
}
