using CarWorkshop.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Interfaces
{
    public interface ICarWorkshopRepository
    {
        Task Create(CarWorkshopForm carWorkshop);
    }
}
