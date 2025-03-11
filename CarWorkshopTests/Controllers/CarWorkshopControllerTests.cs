using Xunit;
using CarWorkshop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using CarWorkshop.Application.Entities;
using Moq;
using MediatR;
using CarWorkshop.Application.Querries.GetAllCarWorkshops;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using FluentAssertions;

namespace CarWorkshop.Controllers.Tests
{
    public class CarWorkshopControllerTests:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CarWorkshopControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact()]
        public async void Index_WithCarworkshops_ReturnsRenderedView()
        {
            List<CarWorkshopForm> carWorkshops = new List<CarWorkshopForm>()
            {
                new CarWorkshopForm()
                {
                    Name="Carworkshop 1"
                },
                new CarWorkshopForm()
                {
                    Name="Carworkshop 2"
                },
                 new CarWorkshopForm()
                {
                    Name="Carworkshop 3"
                },
            };

            Mock<IMediator> mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(x => x.Send(It.IsAny<GetAllCarWorkshopsQuerry>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(carWorkshops);
                        
            HttpClient? client = _factory
                .WithWebHostBuilder(builder=>builder.ConfigureTestServices(services=>services.AddScoped(_=>mediatorMock.Object)))
                .CreateClient();

             HttpResponseMessage response = await client.GetAsync("/CarWorkshop/Index");

            Assert.Equal(HttpStatusCode.OK,response.StatusCode);

            string content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("<h1>Car Workshops</h1>")
                .And.Contain("Carworkshop 1")
                .And.Contain("Carworkshop 2")
                .And.Contain("Carworkshop 3");
        }

        [Fact()]
        public async void Index_WithNoCarworkshops_ReturnsEmptyView()
        {
            List<CarWorkshopForm> carWorkshops = new List<CarWorkshopForm>();

            Mock<IMediator> mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(x => x.Send(It.IsAny<GetAllCarWorkshopsQuerry>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(carWorkshops);

            HttpClient? client = _factory
                .WithWebHostBuilder(builder => builder.ConfigureTestServices(services => services.AddScoped(_ => mediatorMock.Object)))
                .CreateClient();

            HttpResponseMessage response = await client.GetAsync("/CarWorkshop/Index");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string content = await response.Content.ReadAsStringAsync();

            content.Should().NotContain("div class=\"card m-3\""); // check for any element which should be rendered if list had elements.
        }
    }
}