using System.ComponentModel.DataAnnotations;

namespace UniversityManagement.API.Models
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        [Range(0, 100)]
        public decimal Grade { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
