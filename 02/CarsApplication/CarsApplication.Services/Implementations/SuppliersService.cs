namespace CarsApplication.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Models;

    public class SuppliersService : ISuppliersService
    {
        private readonly ApplicationDbContext dataContext;


        public SuppliersService( ApplicationDbContext dbContext)
        {
            this.dataContext = dbContext;
        }

        public void Add(string name, bool isImporter)
        {
            this.dataContext.Suppliers.Add(new Data.Models.PartSupplier() { Name = name, UsesImportedParts = isImporter });
            this.dataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Data.Models.PartSupplier supplier = this.dataContext.Suppliers.Find(id);
            if (supplier == null)
            {
                throw new System.NullReferenceException($"Cannot find supplier with id {id}");
            }

            this.dataContext.Suppliers.Remove(supplier);
            this.dataContext.SaveChanges();
        }

        public void Edit(int id, string name, bool isImporter)
        {
            Data.Models.PartSupplier supplier = this.dataContext.Suppliers.Find(id);
            if (supplier == null)
            {
                throw new System.NullReferenceException($"Cannot find supplier with id {id}");
            }

            supplier.Name = name;
            supplier.UsesImportedParts = isImporter;

            this.dataContext.Entry(supplier).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.dataContext.SaveChanges();
        }

        public IEnumerable<SupplierModel> GetAll() 
            => 
            this.dataContext.Suppliers
                .Select(s => new SupplierModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    IsImporter = s.UsesImportedParts
                })
                .ToList();

        public SupplierModel GetById(int id)
        {
            Data.Models.PartSupplier supplier = this.dataContext.Suppliers.Find(id);
            if (supplier == null)
            {
                throw new System.NullReferenceException($"Cannot find supplier with id {id}");
            }

            return new SupplierModel
            {
                IsImporter = supplier.UsesImportedParts,
                Id = supplier.Id,
                Name = supplier.Name,
            };
        }

        public IEnumerable<SupplierModel> GetSuppliers(bool isLocal) 
            => 
            this.dataContext.Suppliers.Where(s => s.UsesImportedParts == isLocal)
                .Select(s => new SupplierModel
                {
                    CountOfParts = s.Parts.Count,
                    Id = s.Id,
                    Name = s.Name,
                })
                .ToList();
    }
}
