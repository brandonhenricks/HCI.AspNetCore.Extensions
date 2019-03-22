namespace HCI.AspNetCore.Extensions
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    /// <summary>
    /// <see cref="ISession"/> Extensions.
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Set the specified session, key and value.
        /// </summary>
        /// <param name="session">Session.</param>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void Set<T>(this ISession session, string key, T value)
        {
            session?.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// Get the specified session and key.
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="session">Session.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T Get<T>(this ISession session, string key)
        {
            var value = session?.GetString(key);
            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}
