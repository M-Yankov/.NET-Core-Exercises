namespace CarsApplication.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using Models;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> GetAll(OrderType order);

        CustomerDetails GetCustomerDetails(int id);

        void AddCustomer(string name, DateTime dateBorn);

        void EditCustomer(int id, string name, DateTime dateBorn);
    }
}
