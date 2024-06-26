using Microsoft.AspNetCore.Mvc;
using Tazkarti.Models.AuthModels;
using Tazkarti.Services;

namespace Tazkarti.Controllers
{
	public class AuthController : Controller
	{
		//private readonly IAuthService _authService;
  //      public AuthController(IAuthService authService)
  //      {
  //          _authService = authService;
  //      }
		[HttpGet]
        public IActionResult Register()
		{
			 return View();
		}
	}
}
