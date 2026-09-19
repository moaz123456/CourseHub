using UniversityManagement.API.Context;
using UniversityManagement.API.IServices;
using UniversityManagement.API.Services;
using UniversityManagement.API.Models;
using UniversityManagement.API.Repository;
using UniversityManagement.API.Repository.Interfaces;
namespace UniversityManagement.API.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UniversityDbContext _context;
        public UnitOfWork(UniversityDbContext context)
        {
            _context = context;
        }
        Dictionary<Type, object> repos = new Dictionary<Type, object>();
        public  IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if(!repos.ContainsKey(type))
            {
               var repo = new GenericRepository<T>(_context);
               repos.Add(type, repo);
            }
            return (IGenericRepository<T>)repos[type];
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
