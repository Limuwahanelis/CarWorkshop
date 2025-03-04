using CarWorkshop.Application.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Querries
{
    public class GetAllCarWorkshopsQuerry: IRequest<IEnumerable<CarWorkshopForm>>
    {

    }
}
