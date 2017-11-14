namespace CarsApplication.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class PartService : IPartService
    {
        private readonly ApplicationDbContext dbContext;

        public PartService(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        public void Add(string name, decimal price, int supplierId, int? quantity)
        {
            Part part = new Part()
            {
                Name = name,
                Price = price,
                SupplierId = supplierId,
            };

            part.Quantity = quantity ?? 1;

            this.dbContext.Parts.Add(part);
            this.dbContext.SaveChanges();
        }

        public void Delete(int partId)
        {
            Part part = this.dbContext.Parts.Include(p => p.PartCars).FirstOrDefault(p => p.Id == partId);
            if (part == null)
            {
                throw new NullReferenceException($"Cannot find part with Id: {partId}");
            }

            part.PartCars.Clear();
            this.dbContext.SaveChanges();

            this.dbContext.Parts.Remove(part);
            this.dbContext.SaveChanges();
        }

        public void Edit(int id, string name, decimal price)
        {
            Part part = this.dbContext.Parts.Find(id);
            if (part == null)
            {
                throw new NullReferenceException($"Cannot find part with id: {id}");
            }

            this.dbContext.Entry(part).State = EntityState.Modified;
            part.Name = name;
            part.Price = price;

            this.dbContext.SaveChanges();
        }

        public IEnumerable<PartSingleModel> GetAll()
            =>
            this.dbContext.Suppliers
                    .SelectMany(s => s.Parts
                        .Select(p => new PartSingleModel()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Price = p.Price,
                            SupplierName = s.Name
                        })
                     )
                     .ToList();

        public PartSingleModel GetById(int id)
        {
            PartSingleModel model = this.dbContext.Parts
                .Select(p => new PartSingleModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .FirstOrDefault(p => p.Id == id);

            if (model == null)
            {
                throw new NullReferenceException($"Cannot find part with id: {id}");
            }

            return model;
        }
    }
}
