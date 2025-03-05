using CarWorkshop.Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Commands.CreateCarWorkshop
{
    public class EditCarWorkshopCommandValidator: AbstractValidator<EditCarWorkshopCommand>
    {
        public EditCarWorkshopCommandValidator() 
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Please enter description");

            RuleFor(x => x.PhoneNumber)
                    .MinimumLength(8)
                    .MaximumLength(12);
        }

    }
}
