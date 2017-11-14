namespace CarsApplication.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CarsApplication.Data.Constants;

    public class Part
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataModelConstants.StringMaxlength)]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public ICollection<PartCars> PartCars { get; set; } = new List<PartCars>();

        public int SupplierId { get; set; }

        public PartSupplier Supplier { get; set; }
    }
}
