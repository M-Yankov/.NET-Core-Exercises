namespace CarsApplication.Controllers
{
    using System.Collections.Generic;
    using CarsApplication.Models.Parts;
    using CarsApplication.Services.Models;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;

    public class PartsController : Controller
    {
       private readonly ISuppliersService supplierService;
        private readonly IPartService partService;

        public PartsController(ISuppliersService supplierService, IPartService partService)
        {
            this.supplierService = supplierService;
            this.partService = partService;
        }

        public IActionResult Index()
        {
            IEnumerable<PartSingleModel> parts = this.partService.GetAll();
            return this.View(parts);
        }

        [HttpGet]
        public IActionResult Add()
        {
            PartHelperModel model = new PartHelperModel
            {
                Suppliers = this.supplierService.GetAll()
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Add(PartHelperModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Suppliers = this.supplierService.GetAll();
                return this.View(model);
            }

            this.partService.Add(model.Part.Name, model.Part.Price, model.Part.SupplierId, model.Part.Quantity);
            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            this.partService.Delete(id);
            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            PartSingleModel part = this.partService.GetById(id);
            return this.View(new PartEditModel() {Name = part.Name, Price = part.Price });
        }

        [HttpPost]
        public IActionResult Edit(int id, PartEditModel part)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(new PartSingleModel { Id = id, Name = part.Name, Price = part.Price });
            }

            this.partService.Edit(id, part.Name, part.Price);
            return this.RedirectToAction(nameof(Index));
        }
    }
}