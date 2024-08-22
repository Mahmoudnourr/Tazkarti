using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tazkarti.Models.AppUser;

namespace Tazkarti.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "User")]
    public class LogController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LogController(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "User")]
        public IActionResult LoggedIn()
        {
            
                ApplicationUser user = new ApplicationUser();
                user.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
                return View("~/Views/User/LoggedIn.cshtml", user);
            
        }
    }
}
