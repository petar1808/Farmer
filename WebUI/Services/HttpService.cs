

using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WebUI.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private NavigationManager _navigationManager;
        public HttpService(
            HttpClient httpClient, 
            NavigationManager navigationManager)
        {
            this._httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendAsync<T>(request);
        } 


        public async Task<T> PostAsync<T>(string uri, object content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var contentm = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
            request.Content = contentm;
            return await SendAsync<T>(request);
        }

        public async Task<T> PutAsync<T>(string uri, object content)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
            return await SendAsync<T>(request);
        }

        public async Task<T> DeleteAsync<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await SendAsync<T>(request);
        } 

        private async Task<T> SendAsync<T>(HttpRequestMessage request)
        {
            //var test2 = request.Headers.Select(x => x.Value.FirstOrDefault());

            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",
            //    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImJlcm9pY2l0ZUBhYnYuYmciLCJuYW1lIjoiUGV0YXIgSXZhbm92Iiwicm9sZSI6IlVzZXIiLCJuYmYiOjE2NjU2ODU0MTUsImV4cCI6MTY2NTc3MTgxNCwiaWF0IjoxNjY1Njg1NDE1fQ.v9panCbZj9_BDUCchUXUMPdCadYuFgZFjvTcqjAY3Jg"
            //    );

            using (var response = await _httpClient.SendAsync(request))
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("login");
                }

                // To do: add other cases 404, 403

                var content = await response.Content.ReadAsStringAsync();

                var jsonOptions = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<T>(content, jsonOptions)!;
            }
        }
    }

    public interface IHttpService
    {
        Task<T> GetAsync<T>(string uri);

        Task<T> PostAsync<T>(string uri, object content);

        Task<T> PutAsync<T>(string uri, object content);

        Task<T> DeleteAsync<T>(string uri);
    }
}
