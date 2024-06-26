using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Tazkarti.Models.AppUser
{
	public class ApplicationUser:IdentityUser	
	{
		
		[Required,MaxLength(100)]	
		public string? FirstName { get; set; }
		[Required, MaxLength(100)]
		public string? LastName { get; set; }
		public byte[] Photo { get; set; }
	}
}
