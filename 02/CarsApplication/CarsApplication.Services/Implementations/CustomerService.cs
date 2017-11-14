
namespace CarsApplication.Services.Implementations
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using CarsApplication.Data;
    using Contracts;
    using Models;
    using CarsApplication.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext context;

        public CustomerService(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
        }

        public CustomerDetails GetCustomerDetails(int id)
            =>
             this.context.Customers
                .Include(c => c.Sales)
                    .ThenInclude<Customer, Sale, Car>(s => s.Car)
                        .ThenInclude<Customer, Car, ICollection<PartCars>>(car => car.PartCars)
                            .ThenInclude<Customer, PartCars, Part>(cp => cp.Part)
                .Select(cust => new CustomerDetails()
                {
                    Id = cust.Id, 
                    CarsBought = cust.Sales.Count,
                    DateOfBith = cust.DateOfBirth,
                    Name = cust.Name,
                    TotalSpentMoney = cust.Sales.Sum(s => s.Car.PartCars.Sum(cp => cp.Part.Price))
                })
                .FirstOrDefault(cust => id == cust.Id);                

        public IEnumerable<CustomerModel> GetAll(OrderType order)
        {
            IQueryable<Customer> customersQuery = this.context.Customers;

            if (order == OrderType.Ascending)
            {
                customersQuery = customersQuery
                    .OrderBy(c => c.DateOfBirth)
                    .ThenBy(c => c.IsYoungDriver);
            }
            else
            {
                customersQuery = customersQuery
                    .OrderByDescending(c => c.DateOfBirth)
                    .ThenBy(c => c.IsYoungDriver);
            }

            return customersQuery
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    BirthDate = c.DateOfBirth,
                    IsYoungDriver = c.IsYoungDriver,
                    Name = c.Name
                })
                .ToList();
        }

        public void AddCustomer(string name, DateTime dateBorn)
        {
            bool isYoungDriver = this.IsYoungDriver(dateBorn);

            Customer customer = new Customer()
            {
                DateOfBirth = dateBorn,
                IsYoungDriver = isYoungDriver,
                Name = name,
            };

            this.context.Customers.Add(customer);
            this.context.SaveChanges();
        }

        public void EditCustomer(int id, string name, DateTime dateBorn)
        {
            bool isYoungDriver = this.IsYoungDriver(dateBorn);

            Customer customer = this.context.Customers.Find(id);
            if (customer == null)
            {
                throw new NullReferenceException($"Cannot find customer with id: {id}");
            }

            customer.DateOfBirth = dateBorn;
            customer.IsYoungDriver = isYoungDriver;
            customer.Name = name;

            this.context.Entry<Customer>(customer).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        private bool IsYoungDriver(DateTime borndate)
        {
            // We assume the customer has starting drive at 18 age.
            DateTime dateStartedDriving = borndate.AddYears(18);
            bool isYoungDriver = (DateTime.UtcNow.Subtract(dateStartedDriving).Days / 365) < 2;
            return isYoungDriver;
        }
    }
}
