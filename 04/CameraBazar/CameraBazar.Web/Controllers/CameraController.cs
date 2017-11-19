using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CameraBazar.Web.Models.CameraViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using CameraBazar.Data.Enums;

namespace CameraBazar.Web.Controllers
{
    public class CameraController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            this.SetItems();
            return this.View();
        }

        private void SetItems()
        {
            this.ViewBag.Items = new List<SelectListItem>()
            {
                new SelectListItem() { Text = CameraMake.Canon.ToString(), Value = ((int)CameraMake.Canon).ToString() },
                new SelectListItem() { Text = CameraMake.Nikon.ToString(), Value = ((int)CameraMake.Nikon).ToString() },
                new SelectListItem() { Text = CameraMake.Penta.ToString(), Value = ((int)CameraMake.Penta).ToString() },
                new SelectListItem() { Text = CameraMake.Sony.ToString(), Value = ((int)CameraMake.Sony).ToString() },
            };
        }
    }
}