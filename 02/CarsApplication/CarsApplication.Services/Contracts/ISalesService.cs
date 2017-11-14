namespace CarsApplication.Services.Contracts
{
    using System.Collections.Generic;
    using Models;

    public interface ISalesService
    {
        IEnumerable<SaleModel> GetAll();

        SaleModel ById(int id);

        IEnumerable<SaleModel> GetDiscounted();

        IEnumerable<SaleModel> GetDiscounted(double percentage);

        SaleReviewModel GetReviewSale(int carId, int customerId, double discount);

        int AddSale(int carId, int customerId, double discount);
    } 
}
