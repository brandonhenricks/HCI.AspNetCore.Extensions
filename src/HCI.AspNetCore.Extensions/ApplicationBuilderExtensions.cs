namespace HCI.AspNetCore.Extensions
{
    using System;
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// Application builder extensions.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Uses the mvc with additional routes.
        /// </summary>
        /// <returns>The mvc with additional routes.</returns>
        /// <param name="builder">Builder.</param>
        public static IApplicationBuilder UseMvcWithAdditionalRoutes(this IApplicationBuilder builder)
        {
            builder.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{?id}");

                routes.MapRoute(
                    "areaRoute",
                    "{area:exists}/{controller=Home}/{action=Index}/{?id}"
                );
            });

            return builder;
        }

        /// <summary>
        /// Helper method for conditional building.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="predicate"></param>
        /// <param name="compose"></param>
        public static IApplicationBuilder When(this IApplicationBuilder builder, bool predicate, Func<IApplicationBuilder> compose)
        {
            return predicate ? compose() : builder;
        }
    }
}
