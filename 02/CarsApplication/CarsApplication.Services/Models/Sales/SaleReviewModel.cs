namespace CarsApplication.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SaleReviewModel
    {
        public int CarId { get; set; }

        [Display(Name ="Car")]
        public string CarDetails { get; set; }

        public int CustomerId { get; set; }

        [Display(Name ="Customer")]
        public string CustomerName { get; set; }

        [Display(Name ="Discount")]
        public string CalculatedDiscountText { get; set; }

        public double TotalDiscount { get; set; }

        [Display(Name ="Car price")]
        public decimal CarPrice { get; set; }

        [Display(Name ="Car final price")]
        public decimal FinalCarPrice { get; set; }
    }
}
