namespace CarsApplication.Services.Contracts
{
    using System.Collections.Generic;
    using Models;

    public interface ICarService
    {
        IEnumerable<CarModel> GetAll();

        IEnumerable<CarModel> GetCars(string make);

        IEnumerable<CarModelWithParts> GetCarsWithParts();

        IEnumerable<string> GetMakes();

        void Add(string make, string model, long traveledDistance, IEnumerable<int> partIds);
    }
}
