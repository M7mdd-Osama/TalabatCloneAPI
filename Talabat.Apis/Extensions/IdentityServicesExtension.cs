using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;
using Talabat.Repository.Identity;

namespace Talabat.Apis.Extensions
{
	public static class IdentityServicesExtension
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection Services)
		{
			Services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<AppIdentityDbContext>();

			Services.AddAuthentication();
			return Services;
		}
	}
}
