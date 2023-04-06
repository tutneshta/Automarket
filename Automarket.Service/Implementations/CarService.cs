using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Automarket.DAL.Interfaces;
using Automarket.Domain.Enam;
using Automarket.Domain.Entity;
using Automarket.Domain.Response;
using Automarket.Domain.ViewModels.Car;
using Automarket.Service.Interfaces;

namespace Automarket.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Car>>> GetCars()
        {
            var baseResponse = new BaseResponse<IEnumerable<Car>>();

            try
            {
                var cars = await _carRepository.Select();

                if (cars.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                }

                baseResponse.Data = cars;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<Car>>()
                {
                    Description = $"[GetCars] : {e.Message}"
                };
            }
        }

        public async Task<IBaseResponse<Car>> GetCar(int id)
        {
            var baseResponse = new BaseResponse<Car>();

            try
            {
                var car = await _carRepository.Get(id);

                if (car == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.CarNotFound;
                    return baseResponse;
                }

                baseResponse.Data = car;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[GetCar] : {e.Message}",
                    StatusCode = StatusCode.CarNotFound
                };
            }
        }

        public async Task<IBaseResponse<Car>> GetCarByName(string name)
        {
            var baseResponse = new BaseResponse<Car>();

            try
            {
                var car = await _carRepository.GetByName(name);

                if (car == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.CarNotFound;
                    return baseResponse;
                }

                baseResponse.Data = car;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[GetCarByName] : {e.Message}",
                    StatusCode = StatusCode.CarNotFound
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteCar(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var car = await _carRepository.Get(id);

                if (car == null)
                {
                    baseResponse.Description = "автобобиль не найден";
                    baseResponse.StatusCode = StatusCode.CarNotFound;
                    return baseResponse;
                }

                await _carRepository.Delete(car);
            }
            catch (Exception e)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCar] : {e.Message}",
                    StatusCode = StatusCode.CarNotFound
                };
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<CarViewModel>> CreateCar(CarViewModel carViewModel)
        {
            var baseResponse = new BaseResponse<CarViewModel>();

            try
            {
                var car = new Car()
                {
                    Description = carViewModel.Description,
                    Model = carViewModel.Model,
                    Name = carViewModel.Name,
                    Price = carViewModel.Price,
                    Speed = carViewModel.Speed,
                    DateCreate = carViewModel.DateCreate,
                    TypeCar = (TypeCar)Convert.ToInt32(carViewModel.TypeCar)
                };

                await _carRepository.Create(car);
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception e)
            {
                return new BaseResponse<CarViewModel>()
                {
                    Description = $"[CreateCar] : {e.Message}",
                    StatusCode = StatusCode.CarNotFound
                };
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<Car>> Edit(int id, CarViewModel carViewModel)
        {
            var baseResponse = new BaseResponse<Car>();

            try
            {
                var car = await _carRepository.Get(id);

                if (car == null)
                {
                    baseResponse.Description = "автобобиль не найден";
                    baseResponse.StatusCode = StatusCode.CarNotFound;
                    return baseResponse;
                }

                car.Description = carViewModel.Description;
                car.Model = carViewModel.Model;
                car.Name = carViewModel.Name;
                car.Price = carViewModel.Price;
                car.Speed = carViewModel.Speed;
                car.DateCreate = carViewModel.DateCreate;
                car.TypeCar = (TypeCar)Convert.ToInt32(carViewModel.TypeCar);

                await _carRepository.Update(car);
            }
            catch (Exception e)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[Edit] : {e.Message}",
                    StatusCode = StatusCode.CarNotFound
                };
            }

            return baseResponse;
        }
    }
}