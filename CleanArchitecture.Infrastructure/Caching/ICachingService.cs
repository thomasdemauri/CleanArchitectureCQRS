namespace CleanArchitecture.Infrastructure.Caching
{
    public interface ICachingService
    {
        Task SetAsync<T>(string key, T value);
        Task<T?> GetAsync<T>(string key);
    }
}
