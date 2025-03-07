using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.Interfaces;
using CarWorkshop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Commands.CreateCarWorkshopService
{
    public class CreateCarWorkshopServiceCommandHandler : IRequestHandler<CreateCarWorkshopServiceCommand>
    {
        private readonly IUserContext _userContext;
        private readonly ICarWorkshopRepository _carWorkshopRepository;
        private readonly ICarWorkshopServiceRepository _carWorkshopServiceRepository;

        public CreateCarWorkshopServiceCommandHandler(IUserContext userContext,ICarWorkshopRepository carWorkshopRepository,
                ICarWorkshopServiceRepository carWorkshopServiceRepository)
        {
            _userContext = userContext;
            _carWorkshopRepository = carWorkshopRepository;
            _carWorkshopServiceRepository = carWorkshopServiceRepository;
        }

        public async Task Handle(CreateCarWorkshopServiceCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.CarWorkshop workshop = await _carWorkshopRepository.GetByEncodedName(request.CarWorkshopEncodedName!);

            // Add check for people who can send form without the use of the application.
            CurrentUser? user = _userContext.GetCurrentUser();
            bool isEditable = user != null && (workshop.CreatedById == user.Id || user.Roles.Contains("Moderator"));

            if (!isEditable)
            {
                return;
            }

            CarWorkshopService carWorkshopService = new CarWorkshopService()
            {
                Cost=request.Cost,
                Description=request.Description,
                CarWorkshopId=workshop.Id,
            };

            await _carWorkshopServiceRepository.Create(carWorkshopService);
        }
    }
}
