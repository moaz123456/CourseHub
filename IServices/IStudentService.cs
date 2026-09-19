using UniversityManagement.API.DTOs;
using UniversityManagement.API.DTOs.StudentDTOs;
using UniversityManagement.API.Models;
namespace UniversityManagement.API.IServices
{
    public interface IStudentService /*:IGenericRepository<Student>*/
    {
        Task<IEnumerable<StudentDepartmentDto>> GetAll();
        Task<Student> AddStudent(StudentCreateDto std);
        Task Update(int id , StudentUpdateDto std);
        Task<string> Delete(int id);
        Task<StudentAllDetailsDTO> GetStudentDetailsAsync(int id);
       
        Task<IEnumerable<StudentDepartmentDto>> GetStudentsByDepartmentAsync(int departmentId);
    }
}
