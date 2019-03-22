namespace HCI.AspNetCore.Extensions
{
    using System.Text;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.WebUtilities;
    using Newtonsoft.Json;

    /// <summary>
    /// <see cref="HttpResponse"/> Extensions.
    /// </summary>
    public static class HttpResponseExtensions
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        /// <summary>
        /// Writes the object to json string, and updates the response with the correct ContentType.
        /// </summary>
        /// <param name="response">Response.</param>
        /// <param name="obj">Object.</param>
        /// <param name="contentType">Content type.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void WriteJson<T>(this HttpResponse response, T obj, string contentType = null)
        {
            response.ContentType = contentType ?? Constants.ContentTypes.Json;

            using (var writer = new HttpResponseStreamWriter(response.Body, Encoding.UTF8))
            {
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    jsonWriter.CloseOutput = false;
                    jsonWriter.AutoCompleteOnClose = false;
                    Serializer.Serialize(jsonWriter, obj);
                }
            }
        }
    }
}
