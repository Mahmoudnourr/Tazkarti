using Tazkarti.Models.AuthModels;

namespace Tazkarti.Services
{
    public interface IAuthService
	{
		Task<Authmodel> RegistrationAsync(RegisterModel model);
	}
}
