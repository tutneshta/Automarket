using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using Automarket.Domain.ViewModels.Car;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var response = await _carService.GetCars();

            if (response.StatusCode == Domain.Enam.StatusCode.OK)
            {
                return View(response.Data);
            }

            return Redirect("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetCar(int id)
        {
            var response = await _carService.GetCar(id);

            if (response.StatusCode == Domain.Enam.StatusCode.OK)
            {
                return View(response.Data);
            }

            return Redirect("Error");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _carService.DeleteCar(id);

            if (response.StatusCode == Domain.Enam.StatusCode.OK)
            {
                return RedirectToAction("GetCars");
            }

            return Redirect("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetCarByName(string name)
        {
            var response = await _carService.GetCarByName(name);

            if (response.StatusCode == Domain.Enam.StatusCode.OK)
            {
                return View(response.Data);
            }

            return Redirect("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
            {
                return View();
            }

            var response = await _carService.GetCar(id);

            if (response.StatusCode == Domain.Enam.StatusCode.OK)
            {
                return View(response.Data);
            }

            return Redirect("Error");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(CarViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _carService.CreateCar(model);
                }
                else
                {
                    await _carService.Edit(model.Id, model);
                }

                return RedirectToAction("GetCars");
            }

            return Redirect("Error");
        }
        
    }
}