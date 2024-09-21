using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Tazkarti.Models.Sport
{
	public class Team
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(50)]
		[MinLength(3)]
		public string Name { get; set; }
		[Required] 
		[JsonIgnore]
		public string? LogoPath { get; set; }
		[JsonIgnore]

		public List<TournamentTeam> TournamentTeams { get; set; }
		[JsonIgnore]
		public List<MatchTeams> MatchTeams { get; set; }

	}
}
