using FluentValidation;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarWorkshop.Application.Interfaces;
using CarWorkshop.Domain.Entities;
using CarWorkshop.Application.Entities;

namespace CarWorkshop.Application.Commands.CreateCarWorkshop
{
    public class CreateCarWorkshopCommandValidator : AbstractValidator<CreateCarWorkshopCommand>
    {
        public CreateCarWorkshopCommandValidator(ICarWorkshopRepository carWorkshopRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2).WithMessage("Name should have at least 2 characters")
                .MaximumLength(20).WithMessage("Name should have maximum of 20 characters")
                .Custom(async (value, context) =>
                {
                    Domain.Entities.CarWorkshop? exists = carWorkshopRepository.GetByName(value).Result;
                    if (exists != null)
                    {
                        context.AddFailure($"{value} is not unique name for car workshop");// error msg returned
                    }
                });



            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Please enter description");

            RuleFor(x => x.PhoneNumber)
                .MinimumLength(8)
                .MaximumLength(12);
        }
    }
}
