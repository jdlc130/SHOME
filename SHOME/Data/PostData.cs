using System;
using System.Diagnostics;
using System.IO;
using System.Json;
using System.Net;
using System.Threading.Tasks;

namespace SHOME.Data
{
    public class PostData
    {
        public static async Task<JsonValue> FetchAsync(string url)
        {
            // Create an HTTP web request using the URL:
            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "POST";

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
    }
}