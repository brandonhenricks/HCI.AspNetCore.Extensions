namespace HCI.AspNetCore.Extensions
{
    using System.Security.Claims;

    /// <summary>
    /// Identity extensions.
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Checks <see cref="ClaimsIdentity"/> for existing <see cref="Claim"/>.
        /// </summary>
        /// <returns><c>true</c>, if claim was found, <c>false</c> otherwise.</returns>
        /// <param name="claimsIdentity">Claims identity.</param>
        /// <param name="claim">Claim.</param>
        public static bool HasClaim(this ClaimsIdentity claimsIdentity, string claim)
        {
            return claimsIdentity?.FindFirst(claim) != null;
        }
    }
}
