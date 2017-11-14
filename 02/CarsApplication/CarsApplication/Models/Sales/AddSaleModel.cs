namespace CarsApplication.Models.Sales
{
    using System.Linq;
    using System.Collections.Generic;
    using CarsApplication.Services.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.ComponentModel.DataAnnotations;

    public class AddSaleModel
    {
        [Display(Name ="Customer")]
        public int CustomerId { get; set; }

        [Display(Name ="Car")]
        public int CarId { get; set; }

        public double Discount { get; set; }

        public IEnumerable<CustomerModel> AllCustomers { get; set; }

        public IEnumerable<CarModel> AllCars { get; set; }

        public IEnumerable<SelectListItem> CustomerListItems
            => this.AllCustomers == null ?
                Enumerable.Empty<SelectListItem>() :
                this.AllCustomers.Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() });

        public IEnumerable<SelectListItem> CarListItems
            =>  this.AllCars == null ?
                Enumerable.Empty<SelectListItem>() :
                this.AllCars.Select(c => new SelectListItem() { Text = $"{c.Make} {c.Model}", Value = c.Id.ToString() });
    }
}
