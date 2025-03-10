using Xunit;
using CarWorkshop.Application.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using FluentAssertions;

namespace CarWorkshop.Application.ApplicationUser.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ReturnCurrentUser()
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,"1"),
                new Claim(ClaimTypes.Email,"test@example.com"),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Role,"User")
            };
            ClaimsPrincipal user = new ClaimsPrincipal(new ClaimsIdentity(claims,"Test"));
            Mock<IHttpContextAccessor> httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });
            UserContext userContext = new UserContext(httpContextAccessor.Object);

            CurrentUser? currentUser = userContext.GetCurrentUser();

            Assert.NotNull(currentUser);
            Assert.Equal("1", currentUser.Id);
            Assert.Equal("test@example.com", currentUser.Email);
            currentUser.Roles.Should().ContainInOrder("Admin", "User");
        }
    }
}