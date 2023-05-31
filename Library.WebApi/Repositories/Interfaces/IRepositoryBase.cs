using System.Collections.Generic;

namespace Library.WebApi.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<List<T>> GetAllRecordsAsync();
        Task<T> GetRecordByIdAsync(int id);
        Task RemoveRecordAsync(T record);
        Task UpdateRecordAsync(T record);
        Task InsertRecordAsync(T record);
        Task SaveAsync();
        void NullChecker(T? record);
    }
}
