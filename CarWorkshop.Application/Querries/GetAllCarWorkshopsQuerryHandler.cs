using AutoMapper;
using CarWorkshop.Application.Entities;
using CarWorkshop.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Querries
{
    public class GetAllCarWorkshopsQuerryHandler : IRequestHandler<GetAllCarWorkshopsQuerry, IEnumerable<CarWorkshopForm>>
    {
        private readonly IMapper _mapper;
        private readonly ICarWorkshopRepository _repository;
        public GetAllCarWorkshopsQuerryHandler(IMapper mapper, ICarWorkshopRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<CarWorkshopForm>> Handle(GetAllCarWorkshopsQuerry request, CancellationToken cancellationToken)
        {
            IEnumerable<CarWorkshopForm> workshops= _mapper.Map<IEnumerable<CarWorkshopForm>>( await _repository.GetAll());
            return workshops;
        }
    }
}
