using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
	public static class AppIdentityDbContextSeed
	{
		public static async Task SeedUserAsync(UserManager<AppUser> userManager)
		{
			if (!userManager.Users.Any())
			{
				var User = new AppUser()
				{
					DisplayName = "Mohamed Osama",
					Email = "MohamedOsamaAliGomaa@gmail.com",
					UserName = "mohamed_osama",
					PhoneNumber = "01203893166"
				};
				await userManager.CreateAsync(User, "Pa$$w0rd");
			}
		}
	}
}
