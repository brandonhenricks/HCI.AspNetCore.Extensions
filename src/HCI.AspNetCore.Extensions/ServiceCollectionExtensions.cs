
namespace HCI.AspNetCore.Extensions
{
	using System;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Versioning;
	using Microsoft.AspNetCore.Mvc.Versioning.Conventions;

	/// <summary>
	/// Service collection extensions.
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds the custom API versioning. With UrlSegment, Default Version Selector.
		/// </summary>
		/// <returns>The custom API versioning.</returns>
		/// <param name="services">Services.</param>
		/// <param name="convention">Convention.</param>
		/// https://github.com/Microsoft/aspnet-api-versioning/wiki/API-Version-Conventions
		public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services, IControllerConvention convention = null)
		{
			if (services is null) throw new ArgumentNullException(nameof(services));

			services.AddApiVersioning(options =>
			{
				options.ReportApiVersions = true;
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
				options.ApiVersionReader = new UrlSegmentApiVersionReader();

				if (convention != null)
				{
					options.Conventions.Add(convention);
				}
			});

			return services;
		}

		/// <summary>
		/// Adds Custom MvCCore
		/// </summary>
		/// <returns>The custom mv CC ore.</returns>
		/// <param name="services">Services.</param>
		public static IServiceCollection AddCustomMvCCore(this IServiceCollection services)
		{
			services
				.AddMvcCore()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddLowerCaseUrlRouting();

			return services;
		}

		/// <summary>
		/// Configures the cors to Allow All
		/// </summary>
		/// <param name="services">Services.</param>
		public static void ConfigureCors(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("AllowAllOrigins", builder =>

				builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials());
			});
		}

		/// <summary>
		/// Set the LowerCaseUrl Value for Routing
		/// </summary>
		/// <returns>The lower case URL routing.</returns>
		/// <param name="services">Services.</param>
		public static IServiceCollection AddLowerCaseUrlRouting(this IServiceCollection services) =>
			services.AddRouting(options => options.LowercaseUrls = true);
	}
}
