using BlooperFE.Data;
using Newtonsoft.Json;

namespace BlooperFE.Services
{
    public class BlooperService
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri(@"https://localhost:7025/") };

        public async Task<List<BlooperModel>> GetBloopersAsync()
        {
            var response = await client.GetAsync($"blooper/get-all/");
            var content = await response.Content.ReadAsStringAsync();
            var responseBloopers = JsonConvert.DeserializeObject<List<BlooperModel>>(content);
            return responseBloopers;
        }
    }
}
