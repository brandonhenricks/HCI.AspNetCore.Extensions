namespace HCI.AspNetCore.Extensions
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// <see cref="IApplicationBuilder"/> Extensions.
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

        /// <summary>
        /// Adds the universal exception handler.
        /// </summary>
        /// <returns>The universal exception handler.</returns>
        /// <param name="builder">Builder.</param>
        /// <param name="logger">Logger.</param>
        public static IApplicationBuilder AddUniversalExceptionHandler(this IApplicationBuilder builder, ILogger logger)
        {
            builder.UseExceptionHandler(handler =>
            {
                handler.Run(async context =>
                {
                    var exception = context.GetException();

                    logger?.LogError(exception, exception.Message);

                    var problemDetails = CreateProblemDetails("Exception", 500, exception);

                    context.Response.WriteJson(problemDetails, Constants.ContentTypes.Json);
                });
            });

            return builder;
        }

        private static ProblemDetails CreateProblemDetails(string title, int statusCode, Exception exception)
        {
            return new ProblemDetails
            {
                Title = title,
                Status = statusCode,
                Detail = exception.Message,
                Instance = $"urn:myorganization:error:{Guid.NewGuid()}"
            };
        }
    }
}
