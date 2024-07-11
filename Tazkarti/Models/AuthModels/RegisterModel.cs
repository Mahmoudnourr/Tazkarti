using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tazkarti.Models.AuthModels
{
	public class RegisterModel
	{
		[Required,StringLength(100,ErrorMessage ="The First Name Must be less Than 100 char"),]
		public string FirstName { get; set; }
		[Required, StringLength(100, ErrorMessage = "The Last Name Must be less Than 100 char")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "The City is required")]
		public string City { get; set; }
		[Required, StringLength(100)]
		[EmailAddress(ErrorMessage ="Invalid Email address")]
        //[RegularExpression(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", ErrorMessage = "Invalid pattern.")]
        public string Email { get; set; }
		public string Phone { get; set; }
		[Required, StringLength(256),DataType(DataType.Password)]
		[RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",ErrorMessage ="Invalid Password")]
		public string Password { get; set; }
        [Required,Compare("Password",ErrorMessage ="Passwords doesn't matche")]
        public string ConfirmedPassword { get; set; }
        [Required(ErrorMessage ="The User Name is Required"), StringLength(100, ErrorMessage = "The City Must be less Than 100 char")]
        public string UserName { get; set; }
		public byte[]? Photo { get; set; }

	}
}
