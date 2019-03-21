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
					"{area}/{controller=Home}/{action=Index}/{?id}"
				);
			});

			return builder;
		}
	}
}
