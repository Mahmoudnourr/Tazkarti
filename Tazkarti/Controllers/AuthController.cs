using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Tazkarti.Data;
using Tazkarti.DTOS;
using Tazkarti.Models.AppUser;
using Tazkarti.Models.AuthModels;
using Tazkarti.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
		[HttpGet]
		public IActionResult Login()
		{
			LoginModel loginModel = new LoginModel();
			return View("Login",loginModel);
		}
		[HttpPost]
		public async Task<IActionResult> login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				LoginResult res = new LoginResult();
				res= await _authService.LoginAsync(model);
				if (res is not null)
				{
					if (res.Success)
					{
						CookieOptions cookieOptions = new CookieOptions();
						//cookieOptions.Expires = DateTime.Now.AddSeconds(10);
						Response.Cookies.Append("UserName",res.UserName, cookieOptions);
                       
                        return RedirectToAction("LoggedIn", "Log");

                    }
					else
					{
						foreach (var error in res.Errors)
						{
							ModelState.AddModelError(string.Empty, error);
                        }
                    }
                }
				
            }
			
			return View("Login",model);
		}

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _toastNotification.AddSuccessToastMessage("You have been logged out successfully.");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }

}
