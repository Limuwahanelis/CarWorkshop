using Xunit;
using CarWorkshop.Application.Commands.CreateCarWorkshopService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarWorkshop.Domain.Entities;
using Moq;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.Interfaces;

namespace CarWorkshop.Application.Commands.CreateCarWorkshopService.Tests
{
    public class CreateCarWorkshopServiceCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_WhenUserIsAuthorized_CreateNewService()
        {
            Domain.Entities.CarWorkshop carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };
            CreateCarWorkshopServiceCommand command = new CreateCarWorkshopServiceCommand()
            {
                Cost="100 PLN",
                Description = "Service desc",
                CarWorkshopEncodedName="workshop1"
            };


            Mock<IUserContext> userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(x => x.GetCurrentUser()).Returns(new CurrentUser("1", "test@email.com", ["user"]));
            Mock<ICarWorkshopRepository> carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(x => x.GetByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);
            Mock<ICarWorkshopServiceRepository> carWorkshopSericeReopositoryMock = new Mock<ICarWorkshopServiceRepository>();

            CreateCarWorkshopServiceCommandHandler handler = new CreateCarWorkshopServiceCommandHandler(userContextMock.Object
                , carWorkshopRepositoryMock.Object, carWorkshopSericeReopositoryMock.Object);

            Task result =  handler.Handle(command,CancellationToken.None);

            Assert.True(result.IsCompletedSuccessfully);
            carWorkshopSericeReopositoryMock.Verify(x => x.Create(It.IsAny<CarWorkshopService>()),Times.Once);
        }

        [Fact()]
        public async Task Handle_WhenUserIsModerator_CreateNewService()
        {
            Domain.Entities.CarWorkshop carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };
            CreateCarWorkshopServiceCommand command = new CreateCarWorkshopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service desc",
                CarWorkshopEncodedName = "workshop1"
            };


            Mock<IUserContext> userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(x => x.GetCurrentUser()).Returns(new CurrentUser("245", "test@email.com", ["Moderator"]));
            Mock<ICarWorkshopRepository> carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(x => x.GetByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);
            Mock<ICarWorkshopServiceRepository> carWorkshopSericeReopositoryMock = new Mock<ICarWorkshopServiceRepository>();

            CreateCarWorkshopServiceCommandHandler handler = new CreateCarWorkshopServiceCommandHandler(userContextMock.Object
                , carWorkshopRepositoryMock.Object, carWorkshopSericeReopositoryMock.Object);

            Task result = handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsCompletedSuccessfully);
            carWorkshopSericeReopositoryMock.Verify(x => x.Create(It.IsAny<CarWorkshopService>()), Times.Once);
        }

        [Fact()]
        public async Task Handle_WhenUserIsNotAuthorized_DoesntCreateNewService()
        {
            Domain.Entities.CarWorkshop carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };
            CreateCarWorkshopServiceCommand command = new CreateCarWorkshopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service desc",
                CarWorkshopEncodedName = "workshop1"
            };


            Mock<IUserContext> userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(x => x.GetCurrentUser()).Returns(new CurrentUser("245", "test@email.com", ["user"]));
            Mock<ICarWorkshopRepository> carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(x => x.GetByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);
            Mock<ICarWorkshopServiceRepository> carWorkshopSericeReopositoryMock = new Mock<ICarWorkshopServiceRepository>();

            CreateCarWorkshopServiceCommandHandler handler = new CreateCarWorkshopServiceCommandHandler(userContextMock.Object
                , carWorkshopRepositoryMock.Object, carWorkshopSericeReopositoryMock.Object);

            Task result = handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsCompletedSuccessfully);
            carWorkshopSericeReopositoryMock.Verify(x => x.Create(It.IsAny<CarWorkshopService>()), Times.Never);
        }

        [Fact()]
        public async Task Handle_WhenUserIsNotAuthenticated_DoesntCreateNewService()
        {
            Domain.Entities.CarWorkshop carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };
            CreateCarWorkshopServiceCommand command = new CreateCarWorkshopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service desc",
                CarWorkshopEncodedName = "workshop1"
            };


            Mock<IUserContext> userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(x => x.GetCurrentUser()).Returns((CurrentUser?)null);
            Mock<ICarWorkshopRepository> carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(x => x.GetByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);
            Mock<ICarWorkshopServiceRepository> carWorkshopSericeReopositoryMock = new Mock<ICarWorkshopServiceRepository>();

            CreateCarWorkshopServiceCommandHandler handler = new CreateCarWorkshopServiceCommandHandler(userContextMock.Object
                , carWorkshopRepositoryMock.Object, carWorkshopSericeReopositoryMock.Object);

            Task result = handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsCompletedSuccessfully);
            carWorkshopSericeReopositoryMock.Verify(x => x.Create(It.IsAny<CarWorkshopService>()), Times.Never);
        }
    }
}