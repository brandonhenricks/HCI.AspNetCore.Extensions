namespace HCI.AspNetCore.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Security.Principal;
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

        /// <summary>
        /// Gets the <see cref="IIdentity"/>
        /// </summary>
        /// <returns>The identity.</returns>
        /// <param name="httpContext">Http context.</param>
        public static IIdentity GetIdentity(this HttpContext httpContext)
        {
            return httpContext?.User?.Identity;
        }

        /// <summary>
        /// Gets the <see cref="Claim"/> for the current <see cref="IIdentity"/>
        /// </summary>
        /// <returns>The user claims.</returns>
        /// <param name="httpContext">Http context.</param>
        public static IEnumerable<Claim> GetUserClaims(this HttpContext httpContext)
        {
            return httpContext?.User?.Claims;
        }

        /// <summary>
        /// Checks <see cref="HttpContext"/> for <see cref="IIdentity"/>
        /// </summary>
        /// <returns><c>true</c>, if identity was hased, <c>false</c> otherwise.</returns>
        /// <param name="httpContext">Http context.</param>
        public static bool HasIdentity(this HttpContext httpContext)
        {
            return httpContext?.User?.Identity != null;
        }
    }
}
