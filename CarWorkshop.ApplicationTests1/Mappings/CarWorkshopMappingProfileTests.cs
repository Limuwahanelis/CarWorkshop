using Xunit;
using CarWorkshop.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CarWorkshop.Application.ApplicationUser;
using AutoMapper;
using CarWorkshop.Application.Entities;
using CarWorkshop.Domain.Entities;

namespace CarWorkshop.Application.Mappings.Tests
{
    public class CarWorkshopMappingProfileTests
    {
        [Fact()]
        public void MappingProfile_MapCarWorkshopDtoToCarWorkshop()
        {
            Mock<IUserContext> userContextMock = new Mock<IUserContext>();
            userContextMock.
                    Setup(c => c.GetCurrentUser())
                    .Returns(new CurrentUser("1", "test@test.com", ["Moderator"]));
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile(new CarWorkshopMappingProfile(userContextMock.Object)));

            IMapper mapper = configuration.CreateMapper();

            CarWorkshopForm dto = new CarWorkshopForm()
            {
                City = "City",
                PhoneNumber = "12345678",
                PostalCode = "12345",
                Street = "Street",
            };

            Domain.Entities.CarWorkshop result = mapper.Map<Domain.Entities.CarWorkshop>(dto);

            Assert.NotNull(result);
            Assert.NotNull(result.ContactDetails);
            Assert.Equal(dto.City, result.ContactDetails.City);
            Assert.Equal(dto.PhoneNumber, result.ContactDetails.PhoneNumber);
            Assert.Equal(dto.PostalCode, result.ContactDetails.PostalCode);
            Assert.Equal(dto.Street, result.ContactDetails.Street);

        }

        [Fact()]
        public void MappingProfile_CreatedBySameUser_MapCarWorkshopToCarWorkshopDto()
        {
            Mock<IUserContext> userContextMock = new Mock<IUserContext>();
            userContextMock.
                    Setup(c => c.GetCurrentUser())
                    .Returns(new CurrentUser("1", "test@test.com", ["Moderator"]));
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile(new CarWorkshopMappingProfile(userContextMock.Object)));

            IMapper mapper = configuration.CreateMapper();

            Domain.Entities.CarWorkshop carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1",
                ContactDetails = new CarWorkshopContactDetails()
                {
                    City = "City",
                    PhoneNumber = "12345678",
                    PostalCode = "12345",
                    Street = "Street",
                }
            };

            CarWorkshopForm result = mapper.Map<CarWorkshopForm>(carWorkshop);

            Assert.NotNull(result);
            Assert.True(result.IsEditable);
            Assert.Equal(carWorkshop.ContactDetails.City, result.City);
            Assert.Equal(carWorkshop.ContactDetails.PhoneNumber, result.PhoneNumber);
            Assert.Equal(carWorkshop.ContactDetails.PostalCode, result.PostalCode);
            Assert.Equal(carWorkshop.ContactDetails.Street, result.Street);

        }
    }
}