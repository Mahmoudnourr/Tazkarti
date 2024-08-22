    using Microsoft.AspNetCore.Identity;
    using System.IdentityModel.Tokens.Jwt;
    using Tazkarti.Models.AppUser;
    using Tazkarti.Models.AuthModels;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Claims;
    using System.Text;
    using Tazkarti.Helper;
    using Microsoft.Extensions.Options;
    using Tazkarti.DTOS;

    namespace Tazkarti.Services
    {
        public class AuthService : IAuthService
        {
            public readonly UserManager<ApplicationUser> applicationUser;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            public readonly JWT _jwt;
            public AuthService(UserManager<ApplicationUser> _applicationUser, IOptions<JWT> jwt,UserManager<ApplicationUser>userManager,SignInManager<ApplicationUser>signInManager)
            {
                applicationUser = _applicationUser;
                _userManager = userManager;
                _signInManager = signInManager;
                _jwt = jwt.Value;
            }

            public async Task<RegistrationResult> RegistrationAsync(RegisterModel model)
            {
                ApplicationUser applicationUser = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    City = model.City,
                    PhoneNumber = model.Phone,
                    UserName = model.UserName,

                };
                IdentityResult res = await _userManager.CreateAsync(applicationUser, model.Password);
                if (res.Succeeded)
                {
                await _userManager.AddToRoleAsync(applicationUser, "User");
                await _signInManager.SignInAsync(applicationUser, false);
                    return new RegistrationResult
                    {
                        IsAuthorized = true,
                    };
                }
                RegistrationResult result = new RegistrationResult
                {
                    Errors=new List<string>()
                };
                foreach (var er in res.Errors)
                {
                    result.Errors.Add(er.Description);
                }
                return result;
            }
            public async Task<LoginResult> LoginAsync(LoginModel model)
            {
                ApplicationUser user=await _userManager.FindByNameAsync(model.UserName);
                if (user is not null)
                {
                    SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var _Role = await _userManager.GetRolesAsync(user);
                    if (_Role.Contains("Admin"))
                    {
                        return new LoginResult
                        {
                            Success = true,
                            UserName = model.UserName,
                            Role = "Admin"
                        };
                    }
                    else
                    {
                        return new LoginResult
                        {
                            Success = true,
                            UserName = model.UserName,
                            Role = "User"
                        };
                    }
                }
                else
                {
                    return new LoginResult
                    {
                        Success = false,
                        Errors = new List<string> { "Invalid Password" }
                    };
                }
                }
                else {
				    return new LoginResult
				    {
                        Success=false,
					    Errors = new List<string> { "Invalid Username Or Password" }
				    };
			    }
            }

        }
    }

