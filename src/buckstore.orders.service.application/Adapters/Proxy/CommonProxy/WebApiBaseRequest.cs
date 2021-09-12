using System;
using System.Text;
using System.Net.Http;
using ServiceStack.Text;
using System.Threading.Tasks;

namespace buckstore.orders.service.application.Adapters.Proxy.CommonProxy
{
    public abstract class WebApiBaseRequest
    {
        protected readonly HttpClient HttpClient;

        protected WebApiBaseRequest(HttpClient httpClient)
        {
            HttpClient = httpClient;
            HttpClient.Timeout = TimeSpan.FromMinutes(5);
        }

        protected async Task<TResponse> PostApisAsync<TResponse, TRequest>(string requestUrl, TRequest requestData)
            where TResponse : class
        {
            var jsonData = JsonSerializer.SerializeToString(requestData);
            var encodedRequest = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await HttpClient.PostAsync(requestUrl, encodedRequest);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResponse>();
        }

        protected async Task PostApiAsync<TRequest>(string requestUrl, TRequest requestData)
        {
            var jsonData = JsonSerializer.SerializeToString(requestData);
            var encodedRequest = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await HttpClient.PostAsync(requestUrl, encodedRequest);

            response.EnsureSuccessStatusCode();
        }

        protected async Task<TResponse> GetApiAsync<TResponse>(string requestUrl) where TResponse : class
        {
            var response = await HttpClient.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TResponse>();
        }
    }
}