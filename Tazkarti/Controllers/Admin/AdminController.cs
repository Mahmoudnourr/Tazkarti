using Microsoft.AspNetCore.Mvc;
using Tazkarti.Models.AppUser;
using Tazkarti.Models.Sport;
using Tazkarti.Services;

public class AdminController : Controller
{
	private readonly IAdminservice _adminService;

	public AdminController(IAdminservice adminService)
	{
		_adminService = adminService;
	}

	public IActionResult AddTournament()
	{
		Tournament tournament = new Tournament();
		return View("~/Views/Admin/Methods/_AddTournamentPartial.cshtml", tournament);
	}
	[HttpPost]
	public async Task<IActionResult> SavingTournament(Tournament model)
	{
		if (ModelState.IsValid)
		{
			var (success, errors) = await _adminService.SaveTournamentAsync(model);
			if (success)
			{
				return Json(new { success = true });
			}

			return Json(new { success = false, errors });
		}

		// Return validation errors if ModelState is invalid
		var errorsDict = ModelState
			.Where(ms => ms.Value.Errors.Count > 0)
			.ToDictionary(
				kvp => kvp.Key,
				kvp => kvp.Value.Errors.FirstOrDefault()?.ErrorMessage
			);

		return Json(new { success = false, errors = errorsDict });
	}


	public IActionResult AdminView()
	{
		var user = new ApplicationUser(); // Load user data as needed
		return View("~/Views/Admin/AdminView.cshtml", user);
	}
}
