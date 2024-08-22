using Microsoft.AspNetCore.Mvc;
using Tazkarti.Models.AppUser;

namespace Tazkarti.Controllers.Match
{
    public class MatchController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MatchController( IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Matches()
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            return View(user);
        }
        public IActionResult AddTournament() {
            ApplicationUser user = new ApplicationUser();
            user.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            return View("~/Views/Match/AddTournament.cshtml",user);
        }
    }
}
