namespace CarsApplication.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Log
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Message { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Name { get; set; }

        public DateTime DateLogged { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }
}
