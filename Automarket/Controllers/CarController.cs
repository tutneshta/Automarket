using System.Threading.Tasks;
using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var response = await _carRepository.Select();
            var response1 = await _carRepository.GetByName("BMW");
            var response2 = await _carRepository.Get(3);
            var car = new Car()
            {
                Name = "mazda",
                Model = "mazda",
                Speed = 200,
                Price = 20000
            };

            await _carRepository.Create(car);
            await _carRepository.Delete(car);

            return View(response);
        }
    }
}