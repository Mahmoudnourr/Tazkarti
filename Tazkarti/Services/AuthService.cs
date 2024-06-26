using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Tazkarti.Models.AppUser;
using Tazkarti.Models.AuthModels;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Tazkarti.Helper;
using Microsoft.Extensions.Options;
namespace Tazkarti.Services
{
	public class AuthService : IAuthService
	{
		public readonly UserManager<ApplicationUser> applicationUser;
		public readonly JWT _jwt;
        public AuthService(UserManager<ApplicationUser> _applicationUser, IOptions<JWT> jwt)
        {
            applicationUser=_applicationUser;
			_jwt = jwt.Value;
        }
        public async Task<Authmodel> RegistrationAsync(RegisterModel model)
		{
			ApplicationUser appuser=await applicationUser.FindByEmailAsync(model.Email);
			if (appuser is not null)
			{
				return new Authmodel { Message = "The Email Already Registred!" };
			}
			ApplicationUser User=await applicationUser.FindByNameAsync(model.UserName);
			if (User is not null)
			{
				return new Authmodel { Message = "The UserName Is Already Registred!" };
			}
			var user = new ApplicationUser
			{
				UserName = model.UserName,
				Email = model.Email,
				LastName = model.LastName,
				FirstName = model.FirstName,

			};
			var result= await applicationUser.CreateAsync(user,model.Password);
			if (!result.Succeeded) 
			{ 
			     var errors=string.Empty;
				foreach (var error in result.Errors)
				{
					errors += $"{error.Description}{Environment.NewLine}";
				}
				return new Authmodel { Message = errors };
			}
			await applicationUser.AddToRoleAsync(user, "User");
			var JwtsecurityToken = await CreateJwtToken(user);
			return new Authmodel
			{
				Email = user.Email,
				ExpiresOn = JwtsecurityToken.ValidTo,
				IsAuthenticated = true,
				Roles = new List<string> { "User" },
				Token = new JwtSecurityTokenHandler().WriteToken(JwtsecurityToken),
				Username = user.UserName,
			};
		}
		private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
		{
			var userClaims = await applicationUser.GetClaimsAsync(user);
			var roles = await applicationUser.GetRolesAsync(user);
			var roleClaims = new List<Claim>();

			foreach (var role in roles)
				roleClaims.Add(new Claim("roles", role));

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim("uid", user.Id)
			}
			.Union(userClaims)
			.Union(roleClaims);

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
			issuer: _jwt.Issuer,
			audience: _jwt.Audience,
				claims: claims,
				expires: DateTime.Now.AddDays(_jwt.DurationInDays),
				signingCredentials: signingCredentials);

			return jwtSecurityToken;
		}
	}
}

