using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    class DataService
    {
        // Returns Json from website api
        public static async Task<dynamic> GetDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);

            dynamic content = null;

            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                content = JsonConvert.DeserializeObject(json);

            }
            return content;

        }
    }
}
