using Tazkarti.DTOS;
using Tazkarti.Models.Sport;

namespace Tazkarti.Services
{
	public interface ITournamentService
	{
		public Task<SavingResult> SaveTournament(string name, string year);
		public Task<List<Tournament>> SearchTournament(string name);
		public Task<Tournament> GetTournamentById(int id);
		public Task<bool> DeleteTournament(int id);
		
	}

}