using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Tazkarti.Data;
using Tazkarti.DTOS;
using Tazkarti.Models.Sport;

namespace Tazkarti.Services
{
	public class AdminService : IAdminservice
	{
		private readonly TazkartiDB Db;
		public AdminService(TazkartiDB dB)
		{
			Db = dB;
		}

		public TazkartiDB DB { get; }

		public async Task<(bool Success, Dictionary<string, string> Errors)> SaveTournamentAsync(Tournament tournament)
		{
			var errors = new Dictionary<string, string>();

			bool nameExists = await Db.Tournaments.AnyAsync(t => t.Name == tournament.Name);
			if (nameExists)
			{
				errors.Add("Name", "Tournament name already exists.");
			}
			if (string.IsNullOrWhiteSpace(tournament.Name))
			{
				errors.Add("Name", "Tournament name is required.");
			}
			if (!Regex.IsMatch(tournament.Year, @"\d{4}/\d{4}"))
			{
				errors.Add("Year", "Year must be in the format '2024/2025'.");
			}

			if (errors.Any())
			{
				return (false, errors);
			}
			try
			{
				Db.Tournaments.Add(tournament);
				await Db.SaveChangesAsync();
				return (true, null);
			}
			catch (Exception ex)
			{
				errors.Add("General", "An error occurred while saving the tournament.");

				return (false, errors);
			}
		}

	}
}
