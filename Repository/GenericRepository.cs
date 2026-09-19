using Microsoft.EntityFrameworkCore;
using UniversityManagement.API.Context;
using UniversityManagement.API.Repository.Interfaces;

namespace UniversityManagement.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly UniversityDbContext _context;

        public GenericRepository(UniversityDbContext context)
        {
            _context = context;
        }

        public  IQueryable<T> GetAllAsync()
        {
            return  _context.Set<T>().AsQueryable();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public  async Task AddAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}