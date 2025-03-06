using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.Interfaces;
using CarWorkshop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Commands.CreateCarWorkshop
{
    public class EditCarWorkshopCommandHandler : IRequestHandler<EditCarWorkshopCommand>
    {
        private ICarWorkshopRepository _repository;
        private readonly IUserContext _userContext;

        public EditCarWorkshopCommandHandler(ICarWorkshopRepository repository,IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public async Task Handle(EditCarWorkshopCommand request, CancellationToken cancellationToken)
        {
             Domain.Entities.CarWorkshop workshop=await _repository.GetByEncodedName(request.EncodedName!);

            // Add check for people who can send form without the use of the application.
            CurrentUser? user = _userContext.GetCurrentUser();
            bool isEditable = user != null && workshop.CreatedById == user.Id;

            if(isEditable)
            {
                return;
            }

            workshop.Description = request.Description;
            workshop.About = request.About;
            workshop.ContactDetails.City = request.City;
            workshop.ContactDetails.PostalCode = request.PostalCode;
            workshop.ContactDetails.PhoneNumber = request.PhoneNumber;
            workshop.ContactDetails.Street = request.Street;
            await _repository.Update(workshop);
        }
    }
}
