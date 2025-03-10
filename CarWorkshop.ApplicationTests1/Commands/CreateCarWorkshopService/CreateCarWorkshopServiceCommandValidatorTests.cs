using Xunit;
using CarWorkshop.Application.Commands.CreateCarWorkshopService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarWorkshop.Application.Commands.CreateCarWorkshop;
using FluentValidation.TestHelper;

namespace CarWorkshop.Application.Commands.CreateCarWorkshopService.Tests
{
    public class CreateCarWorkshopServiceCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_NoValidationErrors()
        {
            CreateCarWorkshopServiceCommandValidator validator = new CreateCarWorkshopServiceCommandValidator();
            CreateCarWorkshopServiceCommand command = new CreateCarWorkshopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Description",
                CarWorkshopEncodedName = "workshop1"
            };

            TestValidationResult<CreateCarWorkshopServiceCommand> result= validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validate_WithInvalidCommand_ValidationErrors()
        {
            CreateCarWorkshopServiceCommandValidator validator = new CreateCarWorkshopServiceCommandValidator();
            CreateCarWorkshopServiceCommand command = new CreateCarWorkshopServiceCommand()
            {
                Cost = "",
                Description = "",
                CarWorkshopEncodedName = null
            };

            TestValidationResult<CreateCarWorkshopServiceCommand> result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x=>x.Cost);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.CarWorkshopEncodedName);
        }
    }
}