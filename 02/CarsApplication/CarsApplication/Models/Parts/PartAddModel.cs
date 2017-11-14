namespace CarsApplication.Models.Parts
{
    using System.ComponentModel.DataAnnotations;

    public class PartAddModel
    {
        [Required]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int SupplierId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
