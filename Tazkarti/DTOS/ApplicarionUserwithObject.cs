using Tazkarti.Models.AppUser;

namespace Tazkarti.DTOS
{
	public class ApplicarionUserwithObject<T>
	{
		public ApplicationUser User { get; set; }
		public T Object { get; set; }

	}
}
