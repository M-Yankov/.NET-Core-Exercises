namespace CarsApplication.Models.Parts
{
    using System.ComponentModel.DataAnnotations;

    public class PartEditModel
    {
        [Required]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
