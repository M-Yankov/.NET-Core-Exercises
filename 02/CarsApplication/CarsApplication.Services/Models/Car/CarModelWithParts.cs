namespace CarsApplication.Services.Models
{
    using System.Collections.Generic;

    public class CarModelWithParts : CarModel
    {
        public IEnumerable<PartModel> Parts { get; set; } = new List<PartModel>();
    }
}
