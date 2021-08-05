using System.Text;
using System.Net.Http;
using ServiceStack.Text;
using System.Threading.Tasks;

namespace buckstore.orders.service.infrastructure.common.Proxy.Core
{
    public class WebApiBaseRequest
    {
        protected readonly HttpClient HttpClient;

        public WebApiBaseRequest(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        protected void AddHeader(string name, string value)
        {
            HttpClient.DefaultRequestHeaders.Add(name, value);
        }

        protected async Task<TResponse> PostAsync<TResponse, TRequest>(string requestUrl, TRequest dataRequest)
            where TResponse : class
        {
            var jsonDataRequest = JsonSerializer.SerializeToString(dataRequest);
            var encodeRequestData = new StringContent(jsonDataRequest, Encoding.UTF8, "application/json");

            var response = await HttpClient.PatchAsync(requestUrl, encodeRequestData);

            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadAsAsync<TResponse>();
        }
    }
}