using System.ComponentModel.DataAnnotations;

namespace UniversityManagement.API.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<Enrollment> Enrollments{ get; set; }
        public Address Address { get; set; }
    }
}
