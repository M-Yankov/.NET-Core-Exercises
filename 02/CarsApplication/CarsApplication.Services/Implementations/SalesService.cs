namespace CarsApplication.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarsApplication.Data.Models;
    using CarsApplication.Services.Models;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class SalesService : ISalesService
    {
        private readonly ApplicationDbContext dbContext;

        public SalesService(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        public int AddSale(int carId, int customerId, double discount)
        {
            Customer customer = this.dbContext.Customers.Find(customerId);
            if (customer == null)
            {
                throw new NullReferenceException($"Cannot find customer with id: {customerId}");
            }

            Car car = this.dbContext.Cars.Find(carId);
            if (car == null)
            {
                throw new NullReferenceException($"Cannot find car with id: {carId}");
            }

            if (customer.IsYoungDriver)
            {
                discount += 5;
            }

            Sale sale = new Sale()
            {
                CarId = carId,
                CustomerId = customerId,
                DiscountPercentage = discount,
            };

            this.dbContext.Sales.Add(sale);
            this.dbContext.SaveChanges();

            return sale.Id;
        }

        public SaleModel ById(int id)
            =>
            this.dbContext.Sales
                .Select(s => new SaleModel
                {
                    Id = s.Id,
                    Car = new CarModel()
                    {
                        DistanceTravelled = s.Car.TraveledDistanceInKm,
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                    },
                    CustomerName = s.Customer.Name,
                    DiscontPercentage = s.DiscountPercentage,
                    Price = s.Car.PartCars.Sum(p => p.Part.Price)
                })
                .FirstOrDefault(s => s.Id == id);

        public IEnumerable<SaleModel> GetAll()
            =>
            this.dbContext.Sales
                .Select(s => new SaleModel
                {
                    Car = new CarModel()
                    {
                        DistanceTravelled = s.Car.TraveledDistanceInKm,
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                    },
                    CustomerName = s.Customer.Name,
                    DiscontPercentage = s.DiscountPercentage,
                    Price = s.Car.PartCars.Sum(p => p.Part.Price)
                })
                .ToList();

        public IEnumerable<SaleModel> GetDiscounted()
              =>
            this.dbContext.Sales
                .Where(s => s.DiscountPercentage > 0)
                .Select(s => new SaleModel
                {
                    Car = new CarModel()
                    {
                        DistanceTravelled = s.Car.TraveledDistanceInKm,
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                    },
                    CustomerName = s.Customer.Name,
                    DiscontPercentage = s.DiscountPercentage,
                    Price = s.Car.PartCars.Sum(p => p.Part.Price)
                })
                .ToList();

        public IEnumerable<SaleModel> GetDiscounted(double percentage)
              =>
            this.dbContext.Sales
                .Where(s => s.DiscountPercentage == percentage)
                .Select(s => new SaleModel
                {
                    Car = new CarModel()
                    {
                        DistanceTravelled = s.Car.TraveledDistanceInKm,
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                    },
                    CustomerName = s.Customer.Name,
                    DiscontPercentage = s.DiscountPercentage,
                    Price = s.Car.PartCars.Sum(p => p.Part.Price)
                })
                .ToList();

        public SaleReviewModel GetReviewSale(int carId, int customerId, double discountPercentage)
        {
            Customer customer = this.dbContext.Customers.Find(customerId);
            if (customer == null)
            {
                throw new NullReferenceException($"Cannot find customer with id: {customerId}");
            }

            Car car = this.dbContext.Cars
                .Include(c => c.PartCars)
                    .ThenInclude<Car, PartCars, Part>(cp => cp.Part)
                .FirstOrDefault(c => c.Id == carId);
            if (car == null)
            {
                throw new NullReferenceException($"Cannot find car with id: {carId}");
            }

            double totalDiscount = discountPercentage;
            if (customer.IsYoungDriver)
            {
                totalDiscount += 5;
            }

            decimal carPrice = car.PartCars.Sum(p => p.Part.Price);
            decimal finalPrice = carPrice * Convert.ToDecimal(((100.0 - totalDiscount) / 100.0));

            SaleReviewModel reviewModel = new SaleReviewModel()
            {
                CalculatedDiscountText = (discountPercentage / 100).ToString("P2") + (customer.IsYoungDriver ? $"({0.05.ToString("P2")})" : string.Empty),
                CustomerName = customer.Name,
                CarDetails = $"{car.Make} {car.Model}",
                TotalDiscount = totalDiscount / 100,
                CarPrice = carPrice,
                FinalCarPrice = finalPrice,
                CarId = car.Id,
                CustomerId = customer.Id,
            };

            return reviewModel;
        }
    }
}
