    using Microsoft.EntityFrameworkCore;

    namespace UniversityManagement.API.Repository.Interfaces
    {
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAllAsync();
        Task<T?> GetByIdAsync(int id);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        //Task SaveAsync();

    }
}
