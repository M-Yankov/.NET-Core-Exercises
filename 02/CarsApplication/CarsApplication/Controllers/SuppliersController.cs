namespace CarsApplication.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using Services.Models;
    using CarsApplication.Loggers.Filters;

    public class SuppliersController : Controller
    {
        private const string RouteValue = "[controller]/[action]";
        private const string RouteValueWithId = "[controller]/[action]/{id}";
        private readonly ISuppliersService suppliersService;

        public SuppliersController(ISuppliersService service)
        {
            this.suppliersService = service;
        }

        [Route(RouteValue)]
        public IActionResult All()
        {
            return this.View(this.suppliersService.GetAll());
        }

        [Authorize]
        [HttpGet(RouteValue)]
        public IActionResult Add()
        {
            this.ViewData["PostAction"] = nameof(Add);
            return this.View();
        }

        [Authorize]
        [HttpPost(RouteValue)]
        [Log(OperationName = OperationContsants.Add, TablesToBeModified = new[] { "Suppliers" })]
        public IActionResult Add(Models.Suppliers.SupplierInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["PostAction"] = nameof(Add);
                return this.View(model);
            }

            this.suppliersService.Add(model.Name, model.IsImporter);
            return this.RedirectToAction(nameof(All));
        }

        [Authorize]
        [HttpGet(RouteValueWithId)]
        public IActionResult Edit(int id)
        {
            this.ViewData["PostAction"] = nameof(Edit);
            SupplierModel supplier = this.suppliersService.GetById(id);
            return this.View(nameof(Add), new Models.Suppliers.SupplierInputModel() { Name = supplier.Name, IsImporter = supplier.IsImporter });
        }

        [Authorize]
        [HttpPost(RouteValueWithId)]
        [Log(OperationName = OperationContsants.Edit, TablesToBeModified = new[] { "Suppliers" })]
        public IActionResult Edit(int id, Models.Suppliers.SupplierInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["PostAction"] = nameof(Edit);
                return this.View(nameof(Add), model);
            }

            this.suppliersService.Edit(id, model.Name, model.IsImporter);
            return this.RedirectToAction(nameof(All));

        }
        
        [Authorize]
        [HttpPost(RouteValueWithId)]
        [Log(OperationName = OperationContsants.Delete, TablesToBeModified = new[] { "Suppliers", "Part" })]
        public IActionResult Delete(int id)
        {
            this.suppliersService.Delete(id);
            return this.RedirectToAction(nameof(All));
        }

        [Route("[controller]/{supplierType}")]
        public IActionResult Index(string supplierType)
        {
            bool isLocal = true;
            if (!string.IsNullOrEmpty(supplierType) && supplierType.Trim().ToLower() == "importers")
            {
                isLocal = false;
            }

            IEnumerable<SupplierModel> suppliers = this.suppliersService.GetSuppliers(isLocal);
            return this.View(suppliers);
        }
    }
}
