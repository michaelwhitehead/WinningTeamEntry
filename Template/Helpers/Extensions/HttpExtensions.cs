using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Template.Helpers.Exceptions;
using Newtonsoft.Json;
using Polly;
using Xamarin.Essentials;

namespace Template.Helpers.Extensions
{
    public static class HttpExtensions
    {
        public static Task<T> GetAsync<T>(this HttpClient httpClient, string path) where T : class
        {
            CheckNetworkConnectivity();
            return ProcessGetRequest<T>(httpClient, path);
        }

        public static Task<T> GetAndRetryAsync<T>(this HttpClient httpClient, string path, int retryCount, Func<Exception, int, Task> onRetry = null) where T : class
        {
            CheckNetworkConnectivity();
            var func = new Func<Task<T>>(() => ProcessGetRequest<T>(httpClient, path));
            return Retry(func, retryCount, onRetry);
        }

        public static Task<T> GetWaitAndTryAsync<T>(this HttpClient httpClient, string path, Func<int, TimeSpan> sleepDurationProvider, int retryCount, Func<Exception, TimeSpan, Task> onWaitAndRetry = null) where T : class
        {
            CheckNetworkConnectivity();
            var func = new Func<Task<T>>(() => ProcessGetRequest<T>(httpClient, path));
            return WaitAndRetry(func, sleepDurationProvider, retryCount, onWaitAndRetry);
        }

        public static Task<T> PostAsync<T>(this HttpClient httpClient, string path, object data = null) where T : class
        {
            CheckNetworkConnectivity();
            return ProcessPostRequest<T>(httpClient, path, data);
        }

        public static Task<T> PostAndRetryAsync<T>(this HttpClient httpClient, string path, int retryCount, object data = null, Func<Exception, int, Task> onRetry = null) where T : class
        {
            CheckNetworkConnectivity();
            var func = new Func<Task<T>>(() => ProcessPostRequest<T>(httpClient, path, data));
            return Retry(func, retryCount, onRetry);
        }

        public static Task<T> PostWaitAndTryAsync<T>(this HttpClient httpClient, string path, Func<int, TimeSpan> sleepDurationProvider, int retryCount, object data = null, Func<Exception, TimeSpan, Task> onWaitAndRetry = null) where T : class
        {
            CheckNetworkConnectivity();
            var func = new Func<Task<T>>(() => ProcessPostRequest<T>(httpClient, path, data));
            return WaitAndRetry(func, sleepDurationProvider, retryCount, onWaitAndRetry);
        }

        public static Task<T> PutAsync<T>(this HttpClient httpClient, string path, object data = null) where T : class
        {
            CheckNetworkConnectivity();
            return ProcessPutRequest<T>(httpClient, path, data);
        }

        public static Task<T> PutAndRetryAsync<T>(this HttpClient httpClient, string path, int retryCount, object data = null, Func<Exception, int, Task> onRetry = null) where T : class
        {
            CheckNetworkConnectivity();
            var func = new Func<Task<T>>(() => ProcessPutRequest<T>(httpClient, path, data));
            return Retry(func, retryCount, onRetry);
        }

        public static Task<T> PutWaitAndTryAsync<T>(this HttpClient httpClient, string path, Func<int, TimeSpan> sleepDurationProvider, int retryCount, object data = null, Func<Exception, TimeSpan, Task> onWaitAndRetry = null) where T : class
        {
            CheckNetworkConnectivity();
            var func = new Func<Task<T>>(() => ProcessPutRequest<T>(httpClient, path, data));
            return WaitAndRetry(func, sleepDurationProvider, retryCount, onWaitAndRetry);
        }

        public static Task<T> DeleteAsync<T>(this HttpClient httpClient, string path, object data = null) where T : class
        {
            CheckNetworkConnectivity();
            return ProcessDeleteRequest<T>(httpClient, path, data);
        }

        public static Task<T> DeleteAndRetryAsync<T>(this HttpClient httpClient, string path, int retryCount, object data = null, Func<Exception, int, Task> onRetry = null) where T : class
        {
            CheckNetworkConnectivity();
            var func = new Func<Task<T>>(() => ProcessDeleteRequest<T>(httpClient, path, data));
            return Retry(func, retryCount, onRetry);
        }

        public static Task<T> DeleteWaitAndTryAsync<T>(this HttpClient httpClient, string path, Func<int, TimeSpan> sleepDurationProvider, int retryCount, object data = null, Func<Exception, TimeSpan, Task> onWaitAndRetry = null) where T : class
        {
            CheckNetworkConnectivity();
            var func = new Func<Task<T>>(() => ProcessDeleteRequest<T>(httpClient, path, data));
            return WaitAndRetry(func, sleepDurationProvider, retryCount, onWaitAndRetry);
        }

        private static async Task<T> ProcessGetRequest<T>(HttpClient httpClient, string path)
        {
            var response = await httpClient.GetAsync(path);

            var responseContent = await response.Content.ReadAsStringAsync();

            CheckStatusCode(response, responseContent);

            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        private static async Task<T> ProcessPostRequest<T>(HttpClient httpClient, string path, object data = null)
        {
            var content = data is HttpContent httpContent
                ? httpContent
                : new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(path, content);

            var responseContent = await response.Content.ReadAsStringAsync();

            CheckStatusCode(response, responseContent);

            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        private static async Task<T> ProcessPutRequest<T>(HttpClient httpClient, string path, object data = null) where T : class
        {
            var content = data is HttpContent httpContent
                ? httpContent
                : new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(path, content);

            var responseContent = await response.Content.ReadAsStringAsync();

            CheckStatusCode(response, responseContent);

            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        private static async Task<T> ProcessDeleteRequest<T>(HttpClient httpClient, string path, object data = null) where T : class
        {
            var req = new HttpRequestMessage(HttpMethod.Delete, path)
            {
                Content = data is HttpContent content
                ? content
                : new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(req);

            var responseContent = await response.Content.ReadAsStringAsync();

            CheckStatusCode(response, responseContent);

            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        private static Task<T> Retry<T>(Func<Task<T>> func, int retryCount, Func<Exception, int, Task> onRetry)
        {
            onRetry = onRetry ?? OnRetry;
            return Policy
                .Handle<Exception>()
                .RetryAsync(retryCount, onRetry)
                .ExecuteAsync(func);
        }

        private static Task<T> WaitAndRetry<T>(Func<Task<T>> func, Func<int, TimeSpan> sleepDurationProvider, int retryCount, Func<Exception, TimeSpan, Task> onRetryAsync)
        {
            return Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(retryCount, sleepDurationProvider, onRetryAsync)
                .ExecuteAsync(func);
        }

        private static Task OnRetry(Exception exception, int count)
        {
            return Task.CompletedTask;
        }

        private static void CheckNetworkConnectivity()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None || Connectivity.NetworkAccess == NetworkAccess.Unknown)
                throw new ConnectivityException("No Network Connection.");
        }

        private static void CheckStatusCode(HttpResponseMessage response, string content)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new ServerException
                {
                    HttpStatusCode = response.StatusCode,
                    Uri = response.RequestMessage.RequestUri,
                    Content = content
                };
            }
        }
    }
}
