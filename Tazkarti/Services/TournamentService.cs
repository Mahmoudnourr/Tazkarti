using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Tazkarti.Data;
using Tazkarti.DTOS;
using Tazkarti.Models.Sport;

namespace Tazkarti.Services
{
	public class TournamentService : ITournamentService
	{
		public readonly TazkartiDB DB;
		public TournamentService(TazkartiDB _DB)
		{
			DB = _DB;
		}
        public async Task<SavingResult> SaveTournament(string name, string year)
        {
            var result = new SavingResult();
            var errors = new Dictionary<string, string>();

            // Validate name
            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add("Name", "Tournament name is required.");
            }
            else if (name.Any(char.IsDigit))

            {
                errors.Add("Name", "Tournament name cannot contain numbers.");
            }
            else if (await DB.Tournaments.AnyAsync(t => t.Name == name))

            {
                errors.Add("Name", "Tournament name already exists.");
            }


            // Validate year
            if (string.IsNullOrWhiteSpace(year))
            {
                errors.Add("Year", "Year is required.");
            }
            else if (!Regex.IsMatch(year, @"^\d{4}/\d{4}$"))
            {
                errors.Add("Year", "Year must be in the format 'YYYY/YYYY'.");
            }
            else

            {
                var years = year.Split('/');
                var currentYear = DateTime.Now.Year;
                if (int.Parse(years[0]) < currentYear || int.Parse(years[1]) < currentYear)
                {
                    errors.Add("Year", "Year cannot be less than the current year.");
                }
            }


            if (errors.Any())
            {
                result.Success = false;
                result.Errors = errors;
                return result;
            }

            var tournament = new Tournament
            {
                Name = name,
                Year = year
            };

            DB.Tournaments.Add(tournament);
            await DB.SaveChangesAsync();

            result.Success = true;
            return result;
        }
            
        public async Task<List<Tournament>> SearchTournament(string name)
        {
            return await DB.Tournaments.Where(t => t.Name.Contains(name)).ToListAsync();
        }
        public async Task<Tournament> GetTournamentById(int id)
        {
            return await DB.Tournaments.FindAsync(id);
        }
        public async Task<bool> DeleteTournament(int id)
        {
            var tournament = await DB.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return false;
            }
            DB.Tournaments.Remove(tournament);
            await DB.SaveChangesAsync();
            return true;
        }
	}







}