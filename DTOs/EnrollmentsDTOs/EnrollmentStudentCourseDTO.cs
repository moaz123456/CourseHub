namespace UniversityManagement.API.DTOs.EnrollmentsDTOs
{
    public class EnrollmentStudentCourseDTO
    {
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        public decimal Grade { get; set; }
        public DateOnly EnrollmentDate {  get; set; }
    }
}
