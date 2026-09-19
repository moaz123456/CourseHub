namespace UniversityManagement.API.DTOs.EnrollmentsDTOs
{
    public class EnrollmentCreateDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public decimal Grade { get; set; }
    }
}
