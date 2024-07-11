using Tazkarti.DTOS;
using Tazkarti.Models.AuthModels;

namespace Tazkarti.Services
{
    public interface IAuthService
	{
		Task<RegistrationResult> RegistrationAsync(RegisterModel model);
	}
}
