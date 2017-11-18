namespace CarsApplication.Controllers
{
    using CarsApplication.Loggers.Filters;
    using CarsApplication.Models.Cars;
    using CarsApplication.Services.Contracts;
    using CarsApplication.Services.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class CarsController : Controller
    {
        private readonly ICarService carService;
        private readonly IPartService partService;

        public CarsController(ICarService service, IPartService partService)
        {
            this.carService = service;
            this.partService = partService;
        }

        [Route("[controller]/[action]")]
        public IActionResult Parts()
        {
            IEnumerable<CarModelWithParts> carParts = this.carService.GetCarsWithParts();
            return this.View(carParts);
        }

        [Authorize]
        [HttpGet("[controller]/[action]")]
        public IActionResult Add()
        {
            CarHelperModel model = new CarHelperModel
            {
                MakeCollection = this.carService.GetMakes(),
                Parts = this.partService.GetAll()
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost("[controller]/[action]")]
        [Log(OperationName = OperationContsants.Add, TablesToBeModified = new[] { "Cars", "PartCars" })]
        public IActionResult Add(CarHelperModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.MakeCollection = this.carService.GetMakes();
                model.Parts = this.partService.GetAll();
                return this.View(model);
            }

            this.carService.Add(model.Car.Make, model.Car.Model, model.Car.TravveledDistance, model.Car.Parts);
            return this.Redirect($"/Cars/{model.Car.Make}");
        }


        [Route("[controller]/{make}")]
        public IActionResult Index(string make)
        {
            IEnumerable<CarModel> cars = this.carService.GetCars(make.Trim());
            return this.View(cars);
        }
    }
}
