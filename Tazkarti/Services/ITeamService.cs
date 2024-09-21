using Tazkarti.DTOS;
using Tazkarti.Models.Sport;

namespace Tazkarti.Services
{
	public interface ITeamService
	{
		public Task<TeamService> GetTeamById(string teamId);
		public Task<List<TeamService>> GetTeamByName(string teamName);
		public Task DeleteTeam(string teamId);
		public Task<UpdateTeamResult> UpdateTeam(Team team);
		public Task<SavingTeamresult> AddTeam(string name, string logoPath);
	}
}
