using GNB.IBM.Core.Entities.Base;
using GNB.IBM.Core.Interfaces;
using Newtonsoft.Json;

namespace GNB.IBM.Infrastructure.Services
{
    public class HttpHandler<T> : IHttpHandler<T> where T : Entity
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<T>?> GetAsync(string url)
        {
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            List<T>? responseList = JsonConvert.DeserializeObject<List<T>>(json);
            
            return responseList;

        }
    }
}
