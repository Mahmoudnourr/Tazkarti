using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Tazkarti.Data;
using Tazkarti.DTOS;
using Tazkarti.Models.AppUser;
using Tazkarti.Models.AuthModels;
using Tazkarti.Services;

namespace Tazkarti.Controllers
{
	public class AuthController : Controller
	{
        private readonly IAuthService _authService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly IToastNotification _toastNotification;
        public AuthController(UserManager<ApplicationUser>userManager,IAuthService authService,SignInManager<ApplicationUser>signInManager, IToastNotification toastNotification)
        {
            _userManager = userManager;
			_authService = authService;
            _signInManager = signInManager;
			_toastNotification=toastNotification;
        }
        [HttpGet]
        public IActionResult Register()
		{
			RegisterModel registerModel = new RegisterModel();

			 return View(registerModel);
		}
		[HttpPost]
		public async Task<IActionResult> SaveRegister(RegisterModel model)
		{
            if (ModelState.IsValid) 
			{
				RegistrationResult res= await _authService.RegistrationAsync(model);
				if (res.IsAuthorized) {
				 _toastNotification.AddSuccessToastMessage("Registration Completed Successfully");
				 return RedirectToAction("Index","Home");
				}
				else
				{
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }
			
			return View("Register",model);
			
		}
		
	}
}
