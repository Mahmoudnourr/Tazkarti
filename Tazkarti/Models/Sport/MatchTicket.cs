using System.ComponentModel.DataAnnotations.Schema;

namespace Tazkarti.Models.Sport
{
	public class MatchTicket
	{
		[ForeignKey(nameof(Ticket))]
		public int TicketId { get; set; }
		[ForeignKey(nameof(Match))]
		public int MatchId { get; set; }
		public Ticket Ticket { get; set; }
		public Match Match { get; set; }

	}
}
