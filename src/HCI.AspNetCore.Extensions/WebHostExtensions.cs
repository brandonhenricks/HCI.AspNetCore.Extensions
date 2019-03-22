namespace HCI.AspNetCore.Extensions
{
    using Microsoft.AspNetCore.Hosting;
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    /// <summary>
    /// WebHost Extensions Class.
    /// </summary>
	public static class WebHostExtensions
    {
        /// <summary>
        /// Applies the pending database migrations.
        /// </summary>
        /// <returns>The pending database migrations.</returns>
        /// <param name="webHost">Web host.</param>
        /// <typeparam name="T">DbContext Type</typeparam>
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
