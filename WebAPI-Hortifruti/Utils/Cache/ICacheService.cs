namespace WebAPI_Hortifruti.Utils.Cache
{
    internal interface ICacheService
    {
        T Get<T> (string key);
        void Set<T> (string key, T value, int cacheSeconds);
        void Remove (string key);
    }
}
