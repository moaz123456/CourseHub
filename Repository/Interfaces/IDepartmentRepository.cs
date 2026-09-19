using UniversityManagement.API.DTOs.DepartmentsDTOs;
using UniversityManagement.API.Models;
using UniversityManagement.API.DTOs;

namespace UniversityManagement.API.Repository.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<List<DepartmentsWithStudentsDto>> GetAllWithStudents();
    }
}
