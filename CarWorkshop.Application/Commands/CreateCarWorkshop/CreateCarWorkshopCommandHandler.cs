using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
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
        private readonly IUserContext _userContext;

        public CreateCarWorkshopCommandHandler(ICarWorkshopRepository repository, IMapper mapper,IUserContext userContext)
        {
            _carWorkshopRepository = repository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task Handle(CreateCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.CarWorkshop carWorkshop = _mapper.Map<Domain.Entities.CarWorkshop>(request);
            carWorkshop.EncodeName();

            carWorkshop.CreatedById = _userContext.GetCurrentUser().Id;

            await _carWorkshopRepository.Create(carWorkshop);
        }
    }
}
