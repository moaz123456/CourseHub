namespace UniversityManagement.API.DTOs.StudentDTOs
{
    public class StudentCreateDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int DepartmentId { get; set; }
    }
}


