namespace CarsApplication.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Constants.DataModelConstants.StringMaxlength)]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsYoungDriver { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
