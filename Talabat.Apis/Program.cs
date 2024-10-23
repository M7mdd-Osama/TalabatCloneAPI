using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;

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
			//builder.Services.AddScoped<IGenericRepository<Product> , GenericRepository<Product>>();

			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
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
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();


			#endregion

			app.Run();
		}
	}
}
