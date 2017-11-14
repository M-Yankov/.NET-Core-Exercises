namespace CarsApplication.Models.Suppliers
{
    using System.ComponentModel.DataAnnotations;

    public class SupplierInputModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }

        [Display(Name ="Importer ?")]
        public bool IsImporter { get; set; }
    }
}
