using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Apis.Errors;
using Talabat.Apis.Extensions;
using Talabat.Apis.Helpers;
using Talabat.Apis.Middlewares;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.Apis
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Configure Services Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<StoreContext>(Options =>
			{
				Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddDbContext<AppIdentityDbContext>(Options =>
			{
				Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});


			builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
			{
				var Connection = builder.Configuration.GetConnectionString("RedisConnection");

				return ConnectionMultiplexer.Connect(Connection);
			});

			builder.Services.AddApplicationServices();


			#endregion

			var app = builder.Build();

			#region Update-Database
			using var Scope = app.Services.CreateScope();
			var Services = Scope.ServiceProvider;
			var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
			try
			{
				var dbContext = Services.GetRequiredService<StoreContext>();
				await dbContext.Database.MigrateAsync();

				var IdentityDbContext = Services.GetRequiredService<AppIdentityDbContext>();
				await IdentityDbContext.Database.MigrateAsync();
				await StoreContextSeed.SeedAsync(dbContext);
			}
			catch (Exception ex)
			{
				var Logger = LoggerFactory.CreateLogger<Program>();
				Logger.LogError(ex, "An Error Occured During Appling The Migration");
			}
			#endregion


			#region Configure - Configure the HTTP request pipeline.                                                    

			// Configure the HTTP request pipeline.
			app.UseMiddleware<ExceptionMiddleWare>();
			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddlewares();
			}

			app.UseStatusCodePagesWithReExecute("/errors/{0}");

			app.UseHttpsRedirection();

			app.UseAuthorization();
			app.UseStaticFiles();

			app.MapControllers();

			#endregion

			app.Run();
		}
	}
}
