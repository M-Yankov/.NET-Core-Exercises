namespace CarsApplication.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Suppliers")]
    public class PartSupplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Constants.DataModelConstants.StringMaxlength)]
        public string Name { get; set; }

        public bool UsesImportedParts { get; set; }

        public ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}
