using Microsoft.EntityFrameworkCore;
using Tazkarti.Data;
using Tazkarti.DTOS;
using Tazkarti.Models.Sport;

namespace Tazkarti.Services
{
	public class TeamService : ITeamService
	{
		public TazkartiDB _DB;
		public TeamService(TazkartiDB DB)
		{
			_DB = DB;
		}
		public async Task<SavingTeamresult> AddTeam(string name, string logoPath)
		{
			if (string.IsNullOrEmpty(name))
			{

				return new SavingTeamresult
				{
					Message = "The Team Name Is Empty"
				};
			}
			if (await _DB.Teams.FirstOrDefaultAsync(c => c.Name == name) is not null)
			{
				return new SavingTeamresult

				{
					Message = "The Team is Already Existed"
				};
			}
			await _DB.Teams.AddAsync(new Team { Name = name, LogoPath = logoPath });
			try
			{


				await _DB.SaveChangesAsync();
				return new SavingTeamresult
				{
					Message = "The Team Saved Succefully",
					IsSavedSuccefully = true
				};
			}
			catch (Exception ex)
			{
				return new SavingTeamresult { Message = ex.Message };
			}
		}

		public Task DeleteTeam(string teamId)
		{
			throw new NotImplementedException();
		}

		public Task<TeamService> GetTeamById(string teamId)
		{
			throw new NotImplementedException();
		}

		public Task<List<TeamService>> GetTeamByName(string teamName)
		{
			throw new NotImplementedException();
		}

		public Task<UpdateTeamResult> UpdateTeam(Team team)
		{
			throw new NotImplementedException();
		}
	}
}
