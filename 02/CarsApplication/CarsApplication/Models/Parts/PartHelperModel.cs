namespace CarsApplication.Models.Parts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CarsApplication.Services.Models;

    public class PartHelperModel
    {
        public IEnumerable<SupplierModel> Suppliers { get; set; }

        [Required]
        public PartAddModel Part { get; set; }
    }
}
