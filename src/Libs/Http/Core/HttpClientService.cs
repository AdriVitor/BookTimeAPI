using System.Text.Json;
using System.Text;
using Communication.Http.Core.Abstractions;

namespace HttpLib.Requests
{
    public class HttpClientService : IHttpClientService
    {
        public async Task<TResponse> Post<TRequest, TResponse>(TRequest request, string url)
        {
            if(string.IsNullOrEmpty(url))
                throw new ArgumentNullException("Url inválida");

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(url);
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var json = JsonSerializer.Serialize(request);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, stringContent);
                
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Erro na requisição Post. Url: {url} | StatusCode: {response.StatusCode}");

                if (responseBody is null)
                    return default;

                var result = JsonSerializer.Deserialize<TResponse>(responseBody);
                return result;
            }
        }
    }
}
