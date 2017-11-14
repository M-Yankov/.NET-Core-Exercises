namespace CarsApplication.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using CarsApplication.Data.Models;

    public class CarService : ICarService
    {
        private ApplicationDbContext dbContext;

        public CarService(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        public void Add(string make, string model, long traveledDistance, IEnumerable<int> partIds)
        {
            IEnumerable<Part> parts = this.dbContext.Parts.Where(p => partIds.Contains(p.Id)).ToList();

            Car car = new Car()
            {
                Make = make,
                Model = model,
                TraveledDistanceInKm = traveledDistance
            };

            this.dbContext.Cars.Add(car);
            this.dbContext.SaveChanges();

            int counter = 0;
            foreach (Part part in parts)
            {
                car.PartCars.Add(new PartCars() { CarId = car.Id, PartId = part.Id });
                if (counter % 25 == 0 && counter > 0)
                {
                    this.dbContext.SaveChanges();
                }

                counter++;
            }

            this.dbContext.SaveChanges();
        }

        public IEnumerable<CarModel> GetAll()
         =>
            this.dbContext.Cars
                .OrderBy(c => c.Make)
                .Select(c => new CarModel()
                {
                    Make = c.Make,
                    Model = c.Model,
                    Id = c.Id
                })
                .ToList();

        public IEnumerable<CarModel> GetCars(string make) 
            => 
            this.dbContext.Cars
                .Where(c => c.Make == make)
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistanceInKm)
                .Select(c => new CarModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    DistanceTravelled = c.TraveledDistanceInKm
                })
                .ToList();

        /// <summary>
        /// This method causes N + 1. No idea how to fix it.
        /// </summary>
        public IEnumerable<CarModelWithParts> GetCarsWithParts()
            =>
            this.dbContext.Cars
                .Include(c => c.PartCars)
                    .ThenInclude<Car, PartCars, Part>(pc => pc.Part)
                .Select(c => new CarModelWithParts
                {
                    DistanceTravelled = c.TraveledDistanceInKm,
                    Make = c.Make,
                    Model = c.Model,
                    Parts = c.PartCars
                            .Select(p => new PartModel
                            {
                                Name = p.Part.Name,
                                Price = p.Part.Price
                            })
                            .ToList()
                })
                .ToList();

        public IEnumerable<string> GetMakes() 
            => 
            this.dbContext.Cars.OrderBy(c => c.Make).Select(c => c.Make).ToList().Distinct();
    }
}
