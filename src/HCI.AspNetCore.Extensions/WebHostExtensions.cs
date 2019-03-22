namespace HCI.AspNetCore.Extensions
{
    using Microsoft.AspNetCore.Hosting;
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// WebHost Extensions Class.
    /// </summary>
	public static class WebHostExtensions
    {
        /// <summary>
        /// Gets the <see cref="IServiceScopeFactory"/> from the <see cref="IWebHost"/>
        /// </summary>
        /// <returns>The service scope factory.</returns>
        /// <param name="webHost">Web host.</param>
        public static IServiceScopeFactory GetServiceScopeFactory(this IWebHost webHost)
        {
            return webHost.Services.GetService(typeof(IServiceScopeFactory)) as IServiceScopeFactory;
        }

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
                var serviceScopeFactory = webHost.GetServiceScopeFactory();

                if (serviceScopeFactory is null)
                {
                    throw new NullReferenceException(nameof(serviceScopeFactory));
                }

                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    var dbContext = services.GetRequiredService<T>();

                    if (dbContext is null)
                    {
                        throw new NullReferenceException(nameof(dbContext));
                    }

                    dbContext.Database.Migrate();
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
