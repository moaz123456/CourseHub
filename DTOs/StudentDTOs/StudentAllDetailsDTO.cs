using System.ComponentModel.DataAnnotations;
using UniversityManagement.API.Models;

namespace UniversityManagement.API.DTOs.StudentDTOs
{
    public class StudentAllDetailsDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        
        public DateOnly DateOfBirth { get; set; }
        public DepartmentDTO Department { get; set; }
        public decimal Grade { get; set; }
        public string  City { get; set; }
    }
}
