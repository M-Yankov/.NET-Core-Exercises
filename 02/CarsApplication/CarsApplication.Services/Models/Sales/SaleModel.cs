namespace CarsApplication.Services.Models
{
    using System;

    public class SaleModel
    {
        public int Id { get; set; }

        public double SalePriceWithDiscount 
            =>
            Convert.ToDouble(this.Price) - (Convert.ToDouble(this.Price) * this.DiscontPercentage);

        public double DiscontPercentage { get; set; }

        public decimal Price { get; set; }

        public string CustomerName { get; set; }

        public CarModel Car { get; set; }
    }
}
