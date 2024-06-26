using System.ComponentModel.DataAnnotations.Schema;

namespace Tazkarti.Models.Entertainment
{
	public class EventTicket
	{
		[ForeignKey(nameof(Event))]
		public int EventId { get; set; }
		[ForeignKey(nameof(Ticket))]
		public int TicketId { get; set; }
		public Event Event { get; set; }
		public Ticket Ticket { get; set; }

	}
}
