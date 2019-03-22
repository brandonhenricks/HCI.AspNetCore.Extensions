namespace HCI.AspNetCore.Extensions
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// <see cref="IServiceScope"/> Extensions.
    /// </summary>
    public static class ServiceScopeExtensions
    {
        /// <summary>
        /// Gets a service from the <see cref="IServiceProvider"/>
        /// </summary>
        /// <returns>The service.</returns>
        /// <param name="scope">Scope.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T GetService<T>(this IServiceScope scope)
        {
            return scope.ServiceProvider.GetRequiredService<T>();
        }
    }
}
