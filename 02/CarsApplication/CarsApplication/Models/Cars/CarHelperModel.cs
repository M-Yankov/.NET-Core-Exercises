namespace CarsApplication.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Services.Models;

    public class CarHelperModel
    {
        public IEnumerable<PartSingleModel> Parts { get; set; }

        public IEnumerable<string> MakeCollection { get; set; }

        [Required]
        public CarAddModel Car { get; set; }
    }
}
