using Xunit;
using CarWorkshop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using FluentAssertions;

namespace CarWorkshop.Controllers.Tests
{
    public class HomeControllerTests:IClassFixture<WebApplicationFactory<Program>>
    {
        WebApplicationFactory<Program> _factory;
        public HomeControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact()]
        public async void About_ReturnsRenderedView()
        {
            HttpClient? client = _factory.CreateClient();

            HttpResponseMessage response= await client.GetAsync("/Home/About");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("<h1>Tile page about</h1>")
                .And.Contain("<p>About page</p>")
                .And.Contain("<li>Web</li>")
                .And.Contain("<li>Car</li>");

        }
    }
}