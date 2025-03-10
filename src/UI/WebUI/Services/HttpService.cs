﻿using Microsoft.AspNetCore.Components;
using Radzen;
using System.Net;
using System.Text;
using System.Text.Json;
using WebUI.Models;

namespace WebUI.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly NotificationService notificationService;

        public HttpService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            NotificationService notificationService)
        {
            this._httpClient = httpClient;
            _navigationManager = navigationManager;
            this.notificationService = notificationService;
        }

        public async Task<T> GetAsync<T>(string uri, Dictionary<string, string>? queryParams = null)
        {
            if (queryParams != null && queryParams.Any())
            {
                var queryString = new StringBuilder();
                queryString.Append("?");

                foreach (var param in queryParams)
                {
                    queryString.Append($"{WebUtility.UrlEncode(param.Key)}={WebUtility.UrlEncode(param.Value)}&");
                }

                queryString.Length--;

                uri += queryString.ToString();
            }

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

            try
            {
                using (var response = await _httpClient.SendAsync(request))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        _navigationManager.NavigateTo("login");
                    }

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var errorMessages = JsonSerializer.Deserialize<ApiErrorMessage>(
                            await response.Content.ReadAsStringAsync(),
                            jsonOptions)!;

                        notificationService.Notify(new NotificationMessage
                        {
                            Severity = NotificationSeverity.Error,
                            Summary = "Валидационна грешка",
                            Detail = string.Join(", ", errorMessages.Errors),
                            Duration = 10000
                        });

                    }

                    if ((int)response.StatusCode >= 500)
                    {
                        var errorMessages = JsonSerializer.Deserialize<ApiErrorMessage>(
                            await response.Content.ReadAsStringAsync(),
                            jsonOptions)!;

                        notificationService.Notify(new NotificationMessage
                        {
                            Severity = NotificationSeverity.Error,
                            Summary = "Грешка в сървара",
                            Detail = string.Join(", ", errorMessages.Errors),
                            Duration = 10000
                        });
                    }

                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        notificationService.Notify(new NotificationMessage
                        {
                            Severity = NotificationSeverity.Warning,
                            Summary = "Грешка",
                            Detail = "Ресурсът не е открит",
                            Duration = 10000
                        });
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        return (T)Activator.CreateInstance(typeof(T))!;
                    }
                    var content = await response.Content.ReadAsStringAsync();

                    return JsonSerializer.Deserialize<T>(content, jsonOptions)!;
                }
            }
            catch (Exception ex)
            {
                notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Грешка",
                    Detail = ex.Message, // If production "Неуспешно свързване към сървъра"
                    Duration = 10000
                });

                return (T)Activator.CreateInstance(typeof(T))!;
            }
        }
    }

    public interface IHttpService
    {
        Task<T> GetAsync<T>(string uri, Dictionary<string, string>? queryParams = null);

        Task<T> PostAsync<T>(string uri, object content);

        Task<T> PutAsync<T>(string uri, object content);

        Task<T> DeleteAsync<T>(string uri);
    }
}
