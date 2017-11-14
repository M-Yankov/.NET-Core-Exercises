namespace CarsApplication.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CarsApplication.Data.Constants;

    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataModelConstants.StringMaxlength)]
        public string Make { get; set; }

        [Required]
        [MaxLength(DataModelConstants.StringMaxlength)]
        public string Model { get; set; }

        public long TraveledDistanceInKm { get; set; }

        public ICollection<PartCars> PartCars { get; set; } = new List<PartCars>();

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
