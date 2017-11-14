namespace CarsApplication.Services.Contracts
{
    using System.Collections.Generic;
    using Models;

    public interface ISuppliersService
    {
        IEnumerable<SupplierModel> GetSuppliers(bool isLocal);

        IEnumerable<SupplierModel> GetAll();

        SupplierModel GetById(int id);

        void Edit(int id, string name, bool isImporter);

        void Delete(int id);

        void Add(string name, bool isImporter);
    }
}
