using BlooperFE.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Encodings.Web;
using System.Web;

namespace BlooperFE.Services
{
    public class MessageService
    {
        //[Inject]
        //public static HttpClient client { get; set; }
        static HttpClient client = new HttpClient() { BaseAddress = new Uri(@"https://localhost:7025/") };

        public static async Task<string> PostMessageAsync (MessageModel message)
        {
            var apiName = $"message?text={HttpUtility.UrlEncode(message.text)}";

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, apiName);

            //var httpResonse = await client.PostAsync(apiName, null);
            var httpResponse = await client.SendAsync(httpRequest);

            Console.WriteLine("Fetching message");
            Console.WriteLine(httpResponse.Content.ReadAsStringAsync().Result);

            if (httpResponse.IsSuccessStatusCode)
            {
                string _message = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                return _message;
            } else
            {
                Console.WriteLine("Error!");
                return null;
            }
        }
    }
}
