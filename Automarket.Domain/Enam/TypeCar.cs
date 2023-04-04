using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Automarket.Domain.Enam
{
    public enum TypeCar
    {
        [Display (Name = "Легковой автомобиль")]
        PassengerCar = 0,
        [Display (Name = "Седан")]
        Sedan = 1,
        [Display (Name = "Хэтчбэк")]
        HathBack = 2,
        [Display (Name = "Минивэн")]    
        Minivan = 3,
        [Display (Name = "Спортивная машина")]
        SportCar = 4,
        [Display (Name = "Внедорожник")]
        Suv = 5,
    }
}