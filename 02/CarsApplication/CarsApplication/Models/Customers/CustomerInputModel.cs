namespace CarsApplication.Models.Customers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
