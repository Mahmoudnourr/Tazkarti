using System.ComponentModel.DataAnnotations;

namespace Tazkarti.Models.AuthModels
{
	public class RegisterModel
	{
		[Required,StringLength(100)]
		public string FirstName { get; set; }
		[Required, StringLength(100)]
		public string LastName { get; set; }
		[Required, StringLength(100)]
		public string City { get; set; }
		public DateTime DateOfBirth { get; set; }
		[Required, StringLength(100)]
		public string Email { get; set; }
		public string Phone { get; set; }
		[Required, StringLength(256)]

		public string Password { get; set; }
		[Required, StringLength(50)]
		public string UserName { get; set; }

	}
}
