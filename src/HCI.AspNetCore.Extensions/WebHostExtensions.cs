namespace HCI.AspNetCore.Extensions
{
	using Microsoft.AspNetCore.Hosting;
	using System;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.EntityFrameworkCore;
	using System.Linq;

	public static class WebHostExtensions
	{
		public static IWebHost ApplyPendingDatabaseMigrations<T>(this IWebHost webHost) where T : DbContext
		{
			try
			{
				var serviceScopeFactory = (IServiceScopeFactory)webHost.Services.GetService(typeof(IServiceScopeFactory));

				using (var scope = serviceScopeFactory.CreateScope())
				{
					var services = scope.ServiceProvider;
					var dbContext = services.GetRequiredService<T>();

					if (dbContext is null)
					{
						throw new NullReferenceException(nameof(dbContext));
					}

					var pendingMigrations = dbContext.Database.GetPendingMigrations();

					if (pendingMigrations.Any())
					{
						dbContext.Database.Migrate();
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

			return webHost;
		}
	}
}
