using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Tazkarti.DTOS;
using Tazkarti.Models.AppUser;
using Tazkarti.Models.Sport;
using Tazkarti.Services;

public class TournamentController : Controller
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITeamService _TeamtService;
    private readonly IToastNotification _toastNotification;

    private readonly ITournamentService _tournamentService;
    public TournamentController(ITournamentService tournamentService, IHttpContextAccessor httpContextAccessor, ITeamService TeamService, IToastNotification _ToastNotification)
    {

        _httpContextAccessor = httpContextAccessor;
        _TeamtService = TeamService;
        _toastNotification = _ToastNotification;
        _tournamentService = tournamentService;
    }


    public IActionResult AddTournament()
    {

        Tournament tournament = new Tournament();

        return View("~/Views/Admin/Methods/AddTournamentView.cshtml", tournament);
    }
    [HttpPost]
    public async Task<IActionResult> SavingTournament(string name, string year)
    {
        var result = await _tournamentService.SaveTournament(name, year);
        if (result.Success)
        {
            _toastNotification.AddSuccessToastMessage("Tournament added successfully");
            return RedirectToAction("AdminView", "Admin");
        }
        else

        {
            if (result.Errors.ContainsKey("Name"))
            {
                ModelState.AddModelError("Name", result.Errors["Name"]);
            }
            if (result.Errors.ContainsKey("Year"))
            {
                ModelState.AddModelError("Year", result.Errors["Year"]);
            }
            return View("~/Views/Admin/Methods/AddTournamentView.cshtml", new Tournament { Name = name, Year = year });
        }
    }

    public IActionResult SearchTournament()
    {
        return View("~/Views/Admin/Methods/SearchTournament.cshtml", new List<Tournament>());
    }
    [HttpPost]
    public async Task<IActionResult> SearchTournament(string name)

    {
        var result = await _tournamentService.SearchTournament(name);
        return View("~/Views/Admin/Methods/SearchTournament.cshtml", result);
    }
    public async Task<IActionResult> UpdateTournament(int id)
    {
        var result = await _tournamentService.GetTournamentById(id);

        return View("~/Views/Admin/Methods/UpdateTournament.cshtml", result);
    }
    public async Task<IActionResult> DeleteTournament(int id)
    {
        var result = await _tournamentService.DeleteTournament(id);
        if (result)
        {
            _toastNotification.AddSuccessToastMessage("Tournament deleted successfully");
        }
        else
        {
            _toastNotification.AddErrorToastMessage("Failed to delete tournament");
        }
        return RedirectToAction("AdminView", "Admin");
    }

     



}
