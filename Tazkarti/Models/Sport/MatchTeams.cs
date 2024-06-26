using System.ComponentModel.DataAnnotations.Schema;

namespace Tazkarti.Models.Sport
{
	public class MatchTeams
	{
		[ForeignKey("Match")]
		public int MatchId { get; set; }
		[ForeignKey("Team")]
		public int TeamId { get; set; }
		public Match Match { get; set; }
		public Team Team { get; set; }
	}
}
