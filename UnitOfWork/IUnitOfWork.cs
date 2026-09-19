using UniversityManagement.API.IServices;
using UniversityManagement.API.Models;
using UniversityManagement.API.Repository;
using UniversityManagement.API.Repository.Interfaces;
namespace UniversityManagement.API.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;

        //IStudentService Students { get; }
        //IDepartmentRepository Departments { get; }
        //IGenericRepository<Course> Courses { get; }
        Task SaveAsync();
    }
}
