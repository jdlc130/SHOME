using System;
using System.Diagnostics;
using System.Json;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SHOME.Data
{
    public class RequestData
    {
        public static async Task SyncTask(params object[] parameters)
        {
            await FetchAsync(parameters);
        }

        /// <summary>
        /// Gets data from the passed URL.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static async Task<JsonValue> FetchAsync(params object[] parameters)
        {
            var url = parameters.Aggregate("http://" + "montalegre.m-iti.org:22941", (current, parameter) => current + ("/" + parameter));

            // Create an HTTP web request using the URL:
            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";
            
            // Send the request to the server and wait for the response:
            using (var response = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null))
            {
                // Get a stream representation of the HTTP web response:
                using (var stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    var jsonDoc = await Task.Run(() => JsonValue.Load(stream));
                    Debug.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }
        
        /// <summary>
        /// Parse data.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="searchString"></param>
        public static string ParseData(JsonValue json, string searchString)
        {
            JsonValue.Parse(json);

            return "";
        }
    }
}
