using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tazkarti.Models.AppUser;

namespace Tazkarti.Models
{
	public class Ticket
	{
		public int Id { get; set; }
		public string Type { get; set; }
		public int Price { get; set; }
		[ForeignKey("User"),Required]
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
	}
}
