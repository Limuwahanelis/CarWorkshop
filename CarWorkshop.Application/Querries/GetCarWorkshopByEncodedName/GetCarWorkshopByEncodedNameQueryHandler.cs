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

namespace CarWorkshop.Application.Querries.GetCarWorkshopByEncodedName
{
    public class GetCarWorkshopByEncodedNameQueryHandler : IRequestHandler<GetCarWorkshopByEncodedNameQuery, CarWorkshopForm>
    {
        private readonly ICarWorkshopRepository _repository;
        private readonly IMapper _mapper;

        public GetCarWorkshopByEncodedNameQueryHandler(ICarWorkshopRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CarWorkshopForm> Handle(GetCarWorkshopByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.CarWorkshop workshop = await _repository.GetByEncodedName(request.EncodedName);
            return _mapper.Map<CarWorkshopForm>(workshop);
        }
    }
}
