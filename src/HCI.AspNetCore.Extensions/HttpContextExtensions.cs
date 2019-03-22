namespace HCI.AspNetCore.Extensions
{
    using System;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// <see cref="HttpContext"/> Extensions.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Gets the <see cref="IExceptionHandlerFeature"/> from the <see cref="HttpContext"/>
        /// </summary>
        /// <returns>The exception handler feature.</returns>
        /// <param name="httpContext">Http context.</param>
        public static IExceptionHandlerFeature GetExceptionHandlerFeature(this HttpContext httpContext)
        {
            return httpContext?.Features?.Get<IExceptionHandlerFeature>();
        }

        /// <summary>
        /// Gets the <see cref="Exception"/> from the <see cref="IExceptionHandlerFeature"/>
        /// </summary>
        /// <returns>The exception.</returns>
        /// <param name="httpContext">Http context.</param>
        public static Exception GetException(this HttpContext httpContext)
        {
            return httpContext?.GetExceptionHandlerFeature()?.Error;
        }
    }
}
