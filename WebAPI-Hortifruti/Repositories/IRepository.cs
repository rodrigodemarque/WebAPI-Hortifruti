using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI_Hortifruti.Repositories
{
    internal interface IRepository<T> where T : class
    {
        int CacheExpirationTime { get; set; }
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetByNameAsync(string nome);
        Task AddASync(T value);
        Task<bool> UpdateAsync(T value);
        Task<bool> DeleteAsync(int Id);    

    }
}
