using System.Collections.Generic;
using System.Threading.Tasks;
using Automarket.Domain.Entity;
using Automarket.Domain.Response;

namespace Automarket.Service.Interfaces
{
    public interface ICarService
    {
        Task<IBaseResponse<IEnumerable<Car>>> GetCars();
    }
}