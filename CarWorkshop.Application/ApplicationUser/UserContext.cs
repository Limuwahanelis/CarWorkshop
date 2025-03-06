using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.ApplicationUser
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        public CurrentUser? GetCurrentUser()
        {
            ClaimsPrincipal? user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("Context user is not present");
            }

            if(user.Identity==null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            string id = user.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            string email = user.FindFirst(x => x.Type == ClaimTypes.Email)!.Value;
            return new CurrentUser(id, email);
        }
    }
}
