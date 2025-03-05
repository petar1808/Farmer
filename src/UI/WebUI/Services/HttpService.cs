using Microsoft.AspNetCore.Components;
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
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json")
            };
            return await SendAsync<T>(request);
        }

        public async Task<T> PutAsync<T>(string uri, object content)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json")
            };
            return await SendAsync<T>(request);
        }

        public async Task<T> DeleteAsync<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await SendAsync<T>(request);
        }

        private async Task<T> SendAsync<T>(HttpRequestMessage request)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            int maxRetries = 3;
            int initialDelay = 1000; // 1 second

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    using var response = await _httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<T>(content, jsonOptions)!;
                    }

                    if (IsTransientError(response.StatusCode))
                    {
                        if (attempt < maxRetries)
                        {
                            await Task.Delay(initialDelay * (int)Math.Pow(2, attempt - 1));
                            NotifyWarning("Грешка", "Моля изчакайте, опитвам отново да се свържа към сървара!");
                            continue;
                        }
                    }

                    await HandleHttpErrorAsync(response, jsonOptions);
                    return (T)Activator.CreateInstance(typeof(T))!;
                }
                catch (HttpRequestException) when (attempt < maxRetries)
                {
                    NotifyWarning("Грешка", "Моля изчакайте, опитвам отново да се свържа към сървара!");
                    await Task.Delay(initialDelay * (int)Math.Pow(2, attempt - 1));
                }
                catch (Exception ex)
                {
                    NotifyError("Грешка", ex.Message);
                    return (T)Activator.CreateInstance(typeof(T))!;
                }
            }

            NotifyError("Грешка", "Неуспешно свързване към сървъра след няколко опита.");
            return (T)Activator.CreateInstance(typeof(T))!;
        }

        private static bool IsTransientError(HttpStatusCode statusCode)
        {
            return statusCode == HttpStatusCode.RequestTimeout ||
                   statusCode == (HttpStatusCode)429 ||
                   statusCode == HttpStatusCode.BadGateway ||
                   statusCode == HttpStatusCode.ServiceUnavailable ||
                   statusCode == HttpStatusCode.GatewayTimeout;
        }

        private async Task HandleHttpErrorAsync(HttpResponseMessage response, JsonSerializerOptions jsonOptions)
        {
            var content = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _navigationManager.NavigateTo("login");
                    break;

                case HttpStatusCode.BadRequest:
                    NotifyValidationError(content, jsonOptions);
                    break;

                case HttpStatusCode.NotFound:
                    NotifyWarning("Грешка", "Ресурсът не е открит");
                    break;

                default:
                    if ((int)response.StatusCode >= 500)
                    {
                        NotifyServerError(content, jsonOptions);
                    }
                    break;
            }
        }

        private void NotifyValidationError(string content, JsonSerializerOptions jsonOptions)
        {
            var errorMessages = JsonSerializer.Deserialize<ApiErrorMessage>(content, jsonOptions)!;
            notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Валидационна грешка",
                Detail = string.Join(", ", errorMessages.Errors),
                Duration = 6000
            });
        }

        private void NotifyServerError(string content, JsonSerializerOptions jsonOptions)
        {
            var errorMessages = JsonSerializer.Deserialize<ApiErrorMessage>(content, jsonOptions)!;
            notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Грешка в сървара",
                Detail = string.Join(", ", errorMessages.Errors),
                Duration = 6000
            });
        }

        private void NotifyWarning(string summary, string detail)
        {
            notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Warning,
                Summary = summary,
                Detail = detail,
                Duration = 6000
            });
        }

        private void NotifyError(string summary, string detail)
        {
            notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = summary,
                Detail = detail,
                Duration = 6000
            });
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
