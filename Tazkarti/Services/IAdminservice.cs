using Tazkarti.DTOS;
using Tazkarti.Models.Sport;

namespace Tazkarti.Services
{
	public interface IAdminservice
	{
		Task<(bool Success, Dictionary<string, string> Errors)> SaveTournamentAsync(Tournament tournament);
	}
}
