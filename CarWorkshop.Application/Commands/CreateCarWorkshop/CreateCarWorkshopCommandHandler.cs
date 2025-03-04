using AutoMapper;
using CarWorkshop.Application.Entities;
using CarWorkshop.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Commands.CreateCarWorkshop
{
    public class CreateCarWorkshopCommandHandler : IRequestHandler<CreateCarWorkshopCommand>
    {
        private readonly ICarWorkshopRepository _carWorkshopRepository;
        private readonly IMapper _mapper;
        public CreateCarWorkshopCommandHandler(ICarWorkshopRepository repository, IMapper mapper)
        {
            _carWorkshopRepository = repository;
            _mapper = mapper;
        }
        public async Task Handle(CreateCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.CarWorkshop carWorkshop = _mapper.Map<Domain.Entities.CarWorkshop>(request);
            carWorkshop.EncodeName();
            await _carWorkshopRepository.Create(carWorkshop);
        }
    }
}
