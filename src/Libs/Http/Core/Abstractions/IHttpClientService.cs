namespace Communication.Http.Core.Abstractions
{
    public interface IHttpClientService
    {
        Task<TResponse> Post<TRequest, TResponse>(TRequest request, string url);
    }
}
