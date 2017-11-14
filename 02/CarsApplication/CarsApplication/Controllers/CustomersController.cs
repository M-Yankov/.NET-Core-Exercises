namespace CarsApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using CarsApplication.Models.Customers;
    using CarsApplication.Services.Contracts;
    using CarsApplication.Services.Models;
    using Microsoft.AspNetCore.Mvc;

    public class CustomersController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService service)
        {
            this.customerService = service;
        }

        [Route("[controller]/{id}")]
        [Route("[controller]/[action]/{id}")]
        public IActionResult Index(int id)
            => this.View(this.customerService.GetCustomerDetails(id));

        [Route("[controller]/[action]/{order}")]
        public IActionResult All(string order)
        {
            OrderType orderType = OrderType.Ascending;
            if (!string.IsNullOrEmpty(order) && order.Trim().ToLower() == "descending")
            {
                orderType = OrderType.Descending;
            }

            IEnumerable<CustomerModel> customers = this.customerService.GetAll(orderType);
            return this.View(customers);
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult Add()
        {
            this.ViewData["actionName"] = nameof(Add);
            return this.View();
        }

        [HttpPost("[controller]/[action]")]
        public IActionResult Add(CustomerInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["actionName"] = nameof(Add);
                return this.View(model);
            }

            this.customerService.AddCustomer(model.Name, model.DateOfBirth);
            object routeValue = new { order = "descending" };
            return this.RedirectToAction(nameof(All), routeValue);
        }

        [HttpGet("[controller]/[action]/{id}")]
        public IActionResult Edit(int id)
        {
            this.ViewData["actionName"] = nameof(Edit);

            CustomerDetails customerDetails = this.customerService.GetCustomerDetails(id);
            return this.View(nameof(Add), new CustomerInputModel { DateOfBirth = customerDetails.DateOfBith, Name = customerDetails.Name });
        }

        [HttpPost("[controller]/[action]/{id}")]
        public IActionResult Edit(int id, CustomerInputModel customer)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["actionName"] = nameof(Edit);
                return this.View(customer);
            }

            this.customerService.EditCustomer(id, customer.Name, customer.DateOfBirth);
            return this.RedirectToAction(nameof(Index), new { id = id });
        }
    }
}
