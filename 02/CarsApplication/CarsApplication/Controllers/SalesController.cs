namespace CarsApplication.Controllers
{
    using System.Collections.Generic;
    using CarsApplication.Models.Sales;
    using CarsApplication.Services.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;

    public class SalesController : Controller
    {
        private readonly ISalesService salesService;
        private readonly ICustomerService customerService;
        private readonly ICarService carService;

        public SalesController(ISalesService service, ICustomerService customers, ICarService cars)
        {
            this.salesService = service;
            this.customerService = customers;
            this.carService = cars;
        }

        [Route("[controller]/")]
        public IActionResult Index()
        {
            IEnumerable<SaleModel> saleModels = this.salesService.GetAll();
            return this.View(saleModels);
        }

        [Route("[controller]/{id}")]
        public IActionResult Index(int id)
        {
            SaleModel sale = this.salesService.ById(id);
            return this.View("Details", sale);
        }

        [Route("[controller]/[action]")]
        public IActionResult Discounted()
        {
            IEnumerable<SaleModel> saleModels = this.salesService.GetDiscounted();
            return this.View(saleModels);
        }

        [Route("[controller]/[action]/{discount}")]
        public IActionResult Discounted(int discount)
        {
            double discountValue = discount / 100.0;
            IEnumerable<SaleModel> saleModels = this.salesService.GetDiscounted(discountValue);
            return this.View(saleModels);
        }

        /// <summary>
        /// Step 1
        /// </summary>
        [Authorize]
        [Route("[controller]/[action]")]
        public IActionResult Add()
        {
            var model = new AddSaleModel();
            model.AllCars = this.carService.GetAll();
            model.AllCustomers = this.customerService.GetAll(OrderType.Ascending);

            return this.View(model);
        }

        /// <summary>
        /// Step 2
        /// </summary>
        [Authorize]
        [HttpPost("[controller]/[action]")]
        public IActionResult ReviewSale(Models.Sales.AddSaleModel addSale)
            =>
            this.View(this.salesService.GetReviewSale(addSale.CarId, addSale.CustomerId, addSale.Discount));

        /// <summary>
        /// Step 3
        /// </summary>
        [Authorize]
        [HttpPost("[controller]/[action]")]
        public IActionResult AddSale(AddSaleModel addSale)
        {
            int saleId = this.salesService.AddSale(addSale.CarId, addSale.CustomerId, addSale.Discount);

            return this.Redirect($"/Sales/{saleId}");
        }
    }
}
