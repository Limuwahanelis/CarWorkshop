using Xunit;
using CarWorkshop.Application.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.ApplicationUser.Tests
{
    public class CurrentUserTests
    {
        [Fact()]
        public void IsInRole_WithMatching_Role_ReturnsTrue()
        {
            CurrentUser user = new CurrentUser("testId", "test@test.com", new List<string>() { "Admin", "User" });

            bool isInRole= user.IsInRole("Admin");

            Assert.True(isInRole);

        }
        [Fact()]
        public void IsInRole_WithNotMatchingRole_ReturnsFalse()
        {
            CurrentUser user = new CurrentUser("testId", "test@test.com", new List<string>() { "Admin", "User" });

            bool isInRole = user.IsInRole("Moderator");

            Assert.False(isInRole);

        }
        [Fact()]
        public void IsInRole_WithNotMatchingLetterCase_ReturnsFalse()
        {
            CurrentUser user = new CurrentUser("testId", "test@test.com", new List<string>() { "Admin", "User" });

            bool isInRole = user.IsInRole("admin");

            Assert.False(isInRole);

        }
    }
}