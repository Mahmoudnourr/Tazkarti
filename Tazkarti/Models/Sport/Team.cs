namespace Tazkarti.Models.Sport
{
	public class Team
	{ 
		public int Id {  get; set; }
		public string Name { get; set; }
		public byte[]? Logo { get; set; }
		public List<TournamentTeam> TournamentTeams { get; set; }
		public List<MatchTeams> MatchTeams { get; set; }
	}
}
