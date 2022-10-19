

using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WebUI.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly IJSRuntime JSRuntime;
        private readonly NotificationService notificationService;

        public HttpService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            IJSRuntime jSRuntime,
            NotificationService notificationService)
        {
            this._httpClient = httpClient;
            _navigationManager = navigationManager;
            JSRuntime = jSRuntime;
            this.notificationService = notificationService;
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
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("login");
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorcontent = await response.Content.ReadAsStringAsync();
                    
                    notificationService.Notify(new NotificationMessage { 
                        Severity = NotificationSeverity.Error, 
                        Summary = "Грешка", 
                        Detail = errorcontent, 
                        Duration = 10000 
                    });

                }

                // To do: add other cases 404, 403

                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return (T)Activator.CreateInstance(typeof(T))!;
                }

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
