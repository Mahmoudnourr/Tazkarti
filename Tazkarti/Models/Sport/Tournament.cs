namespace Tazkarti.Models.Sport
{
	public class Tournament
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Year { get; set; }
		public List<Match>? Matches { get; set; }
		public List<TournamentTeam>? TournamentTeams { get; set; }
	}
}
