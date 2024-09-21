using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Tazkarti.DTOS;
using Tazkarti.Models.AppUser;
using Tazkarti.Models.Sport;
using Tazkarti.Services;

public class AdminController : Controller
{
	IHttpContextAccessor _httpContextAccessor;
	private readonly ITeamService _TeamtService;
	private readonly IToastNotification _toastNotification;
	public AdminController( IHttpContextAccessor httpContextAccessor, ITeamService TeamService, IToastNotification _ToastNotification)
	{
		_httpContextAccessor = httpContextAccessor;
		_TeamtService = TeamService;
		_toastNotification = _ToastNotification;
	}
	public IActionResult AdminView()

	{
		var user = new ApplicationUser();
		user.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
		return View("~/Views/Admin/AdminView.cshtml", user);
	}
	/*public IActionResult Crud()
	{

		return View("~/Views/Admin/Methods/_crud.cshtml");

	}
	public async Task<IActionResult> Search(string name)
	{
		List<Tournament> TList = await _adminService.Search(name);
		if (TList != null && TList.Count > 0)
		{
			return PartialView("~/Views/Admin/Methods/_TournamentsList.cshtml", TList);
		}
		return PartialView("~/Views/Admin/Methods/_TournamentsList.cshtml", new List<Tournament>()); // Return empty list to avoid null issues
	}
	public async Task<IActionResult> Update(int id)
	{
		ApplicarionUserwithObject<Tournament> model = new ApplicarionUserwithObject<Tournament>();
		ApplicationUser applicationUser = new ApplicationUser();
		model.User = applicationUser;
		Tournament tournament = await _adminService.GetById(id);
		model.Object = tournament;
		model.User.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
		return PartialView("~/Views/Admin/Methods/_UpdateTournament.cshtml", model);
	}
	public async Task<IActionResult> SavingUpdate(Tournament _tournament)
	{
		try
		{
			Tournament t = await _adminService.UpdatingTournament(_tournament);
			return RedirectToAction("AdminView");
		}
		catch (Exception ex)
		{
			return PartialView("~/Views/Admin/Methods/_UpdateTournament.cshtml");
		}

	}

	[HttpGet]
	public async Task<IActionResult> AddTeam()
	{
		Team team = new Team();
		return View("~/Views/Admin/Methods/AddTeamView.cshtml", team);
	}
	[HttpPost]
	public async Task<IActionResult> AddTeam(string Name, IFormFile Logo)
	{
		// handle it in a normal way 
		if (string.IsNullOrEmpty(Name))
		{
			ModelState.AddModelError("Name", "Team name is required.");
			return View("~/Views/Admin/Methods/AddTeamView.cshtml", new Team());
		}

		if (Logo == null || Logo.Length == 0)
		{
			ModelState.AddModelError("Logo", "Logo image is required.");
			return View("~/Views/Admin/Methods/AddTeamView.cshtml", new Team());
		}

		var team = new Team { Name = Name };

		// Save the logo image
		if (Logo != null && Logo.Length > 0)
		{
			var fileName = Path.GetFileName(Logo.FileName);
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "teams", fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await Logo.CopyToAsync(stream);
			}

			team.LogoPath = "/images/teams/" + fileName;
		}

		// Save the team to the database
		var result = await _TeamtService.AddTeam(Name, team.LogoPath);
		if (result.IsSavedSuccefully)
		{
			_toastNotification.AddSuccessToastMessage("Team added successfully");
			return RedirectToAction("AdminView", "Auth");
		}
		else
		{
			_toastNotification.AddErrorToastMessage("Failed to add team");
			return View("~/Views/Admin/Methods/AddTeamView.cshtml", new Team());
		}
	
	}

*/
}
