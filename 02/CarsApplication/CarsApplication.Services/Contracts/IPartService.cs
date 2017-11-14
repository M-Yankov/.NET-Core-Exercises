namespace CarsApplication.Services.Contracts
{
    using System.Collections.Generic;
    using Models;

    public interface IPartService
    {
        void Add(string name, decimal price, int supplierId, int? quantity);

        IEnumerable<PartSingleModel> GetAll();

        void Delete(int partId);

        PartSingleModel GetById(int id);

        void Edit(int id, string name, decimal price);
    }
}
