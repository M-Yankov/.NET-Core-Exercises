namespace CarsApplication.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CarAddModel
    {
        [Required]
        public string Make { get; set; }

        [Required]
        [MinLength(2)]
        public string Model { get; set; }
        
        [Display(Name ="Traveled distance in KM")]
        public long TravveledDistance { get; set; }

        [Required]
        public IEnumerable<int> Parts { get; set; }
    }
}
