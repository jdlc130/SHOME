using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SHOME
{
    public class DataService
    {
        public static async Task<JContainer> getDataFromService(string queryString)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(queryString);

            JContainer data = null;
            if (response != null)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                data = (JContainer) JsonConvert.DeserializeObject(json);
            }

            return data;
        }
    }
}