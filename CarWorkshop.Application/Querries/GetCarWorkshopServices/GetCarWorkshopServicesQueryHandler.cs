using AutoMapper;
using CarWorkshop.Application.Entities;
using CarWorkshop.Application.Interfaces;
using CarWorkshop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Querries.GetCarWorkshopServices
{
    public class GetCarWorkshopServicesQueryHandler : IRequestHandler<GetCarWorkshopServicesQuery, IEnumerable<CarWorkshopServiceDto>>
    {
        private readonly ICarWorkshopServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public GetCarWorkshopServicesQueryHandler(ICarWorkshopServiceRepository serviceRepository,IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CarWorkshopServiceDto>> Handle(GetCarWorkshopServicesQuery request, CancellationToken cancellationToken)
        {
            
            IEnumerable<CarWorkshopService> result =await _serviceRepository.GetAllByEncodedName(request.EncodedName);
            IEnumerable<CarWorkshopServiceDto> dtos = _mapper.Map<IEnumerable<CarWorkshopServiceDto>>(result);
            return dtos;
        }
    }
}
