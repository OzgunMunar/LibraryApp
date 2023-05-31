using Library.WebApi.Models;
using Library.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.WebApi.Repositories.RepositoryClasses
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        private SchoolLibraryDbContext _context;
        private DbSet<T> Entity;

        public RepositoryBase(SchoolLibraryDbContext context)
        {
            this._context = context;
            Entity = _context.Set<T>();
        }

        public async Task<List<T>> GetAllRecordsAsync()
        {
            return await Entity.ToListAsync();
        }

        public async Task<T> GetRecordByIdAsync(int id)
        { 
            T? record = await Entity.FindAsync(id);

            NullChecker(record);
            return record;
        }

        public async Task InsertRecordAsync(T record)
        {
            NullChecker(record);

            await Entity.AddAsync(record);
            await SaveAsync();
        }

        public async Task RemoveRecordAsync(T record)
        {
            Entity.Remove(record);
            await SaveAsync();
        }

        public async Task UpdateRecordAsync(T record)
        {

            Entity.Attach(record);
            Entity.Entry(record).State = EntityState.Modified;

            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Dummy method to check if the incoming value null. Please do not use in any class since it won't do anything.
        /// </summary>
        /// <param name="record"></param>
        /// <exception cref="ArgumentException"></exception>
        public void NullChecker(T? record)
        {
            if (record is null)
            {
                throw new ArgumentException("Record is not found.");
            }
        }

    }
}
