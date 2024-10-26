using System.Runtime.CompilerServices;

namespace Talabat.Apis.Extensions
{
	public static class AddSwaggerExtensions
	{
		public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
		{
			app.UseSwagger();
			app.UseSwaggerUI();
			return app;
		}
	}
}
