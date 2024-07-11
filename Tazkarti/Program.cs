using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NToastNotify;
using System.Text;
using Tazkarti.Data;
using Tazkarti.Helper;
using Tazkarti.Models.AppUser;
using Tazkarti.Services;

namespace Tazkarti
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Bind JWT settings
			builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			// Add DbContext
			builder.Services.AddDbContext<TazkartiDB>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
			);
			
			// Add Identity services
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<TazkartiDB>();

			// Add Authentication
			builder.Services.AddAuthentication(options => {
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(o =>
			{
				o.RequireHttpsMetadata = false;
				o.SaveToken = false;
				o.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateIssuerSigningKey = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidIssuer = builder.Configuration["JWT:Issuer"],
					ValidAudience = builder.Configuration["JWT:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
				};
			});

			// Dependency Injection
			builder.Services.AddScoped<IAuthService, AuthService>();
            // add toaster 
            builder.Services.AddMvc().AddNToastNotifyToastr(new NToastNotify.ToastrOptions()
            {
                ProgressBar = true,
                PositionClass = ToastPositions.TopRight,
                PreventDuplicates = true,
                CloseButton = true,
            });

            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();
            app.UseNToastNotify();
            app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
