using CarWorkshop.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Interfaces
{
    public interface ICarWorkshopRepository
    {
        Task Create(CarWorkshopForm carWorkshop);
        Task<IEnumerable<CarWorkshopForm>> GetAll();
        Task<Domain.Entities.CarWorkshop?> GetByName(string name);
    }
}
