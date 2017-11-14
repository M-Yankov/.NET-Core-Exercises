namespace CarsApplication.Services.Models
{
    public class SupplierModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsImporter { get; set; }

        public long CountOfParts { get; set; }
    }
}
