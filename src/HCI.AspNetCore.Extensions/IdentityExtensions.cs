namespace HCI.AspNetCore.Extensions
{
    using System.Security.Claims;
    using System.Linq;
    using System;
    /// <summary>
    /// Identity extensions.
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Detect if the <paramref name="claimsIdentity"/> has a <see cref="Claim"/> of <paramref name="claimType"/>.
        /// </summary>
        /// <returns><c>true</c>, if claim was found, <c>false</c> otherwise.</returns>
        /// <param name="claimsIdentity">Claims identity.</param>
        /// <param name="claimType">Claim.</param>
        public static bool HasClaim(this ClaimsIdentity claimsIdentity, string claimType)
        {
            return claimsIdentity?.FindFirst(claimType) != null;
        }

        /// <summary>
        /// Detect if the <paramref name="principal"/> has a <see cref="Claim"/> of <paramref name="claimType"/>.
        /// </summary>
        /// <returns><c>true</c>, if claim was hased, <c>false</c> otherwise.</returns>
        /// <param name="principal">Principal.</param>
        /// <param name="claimType">Claim type.</param>
        public static bool HasClaim(this ClaimsPrincipal principal, string claimType)
        {
            if (principal is null) return false;

            if (string.IsNullOrEmpty(claimType))
            {
                throw new ArgumentNullException(nameof(claimType));
            }

            return principal.Claims.Any(x => x.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Returns the <see cref="Claim"/> value of <paramref name="claimType"/> for <paramref name="principal"/>.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="principal">Principal.</param>
        /// <param name="claimType">Claim type.</param>
        public static string GetValue(this ClaimsPrincipal principal, string claimType)
        {
            if (principal is null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            if (string.IsNullOrEmpty(claimType))
            {
                throw new ArgumentNullException(nameof(claimType));
            }

            var claim = principal.Claims.FirstOrDefault(x => x.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase));
            return claim?.Value;
        }
    }
}
