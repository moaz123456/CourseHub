using UniversityManagement.API.Models;
using UniversityManagement.API.DTOs.StudentDTOs;
namespace UniversityManagement.API.DTOs.DepartmentsDTOs
{
    public class DepartmentsWithStudentsDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public List<StudentDepartmentDto> Students { get; set; }
    }
}
