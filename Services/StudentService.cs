using Microsoft.EntityFrameworkCore;
using UniversityManagement.API.Context;
using UniversityManagement.API.Models;
using UniversityManagement.API.DTOs;
using UniversityManagement.API.DTOs.StudentDTOs;
using UniversityManagement.API.UnitOfWork;
using UniversityManagement.API.IServices;
using SendGrid.Helpers.Errors.Model;
namespace UniversityManagement.API.Services
{
    public class StudentService : /*GenericRepository<Student> ,*/ IStudentService
    {
        private readonly UniversityDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public StudentService(UniversityDbContext context , IUnitOfWork unit)
        {
            _context = context;
            _unitOfWork = unit;
        }
        public async Task<IEnumerable<StudentDepartmentDto>> GetAll()
        {
            return await _unitOfWork.Repository<Student>().GetAllAsync().Select(s => new StudentDepartmentDto { 
            FullName = s.FullName,
            Email = s.Email,
            DepartmentName = s.Department.Name
            }).ToListAsync();

        }

        public async Task<Student> AddStudent(StudentCreateDto std)
        {

            var student = new Student
            {
                FullName = std.FullName,
                Email = std.Email,
                DateOfBirth = std.DateOfBirth,
                DepartmentId = std.DepartmentId
            };

             await _unitOfWork.Repository<Student>().AddAsync(student);

            await _unitOfWork.SaveAsync();
            return student;
        }

        public async Task<StudentAllDetailsDTO> GetStudentDetailsAsync(int id)
        {
            var result = await _context.Students.Select(s => new StudentAllDetailsDTO
            {
                Id = s.Id,
                FullName = s.FullName,
                Email = s.Email,
                DateOfBirth = s.DateOfBirth,
                Grade = s.Enrollments.Select(e => e.Grade).FirstOrDefault(),
                City = s.Address.City,
                Department = new DepartmentDTO
                {
                    Id = s.DepartmentId,
                    DepartmentName = s.Department.Name
                }
            }).FirstOrDefaultAsync();

            return result;

        }
        public async Task<IEnumerable<StudentDepartmentDto>> GetStudentsByDepartmentAsync(int departmentId)
        {
            return await _context.Students
                .Where(s => s.DepartmentId == departmentId)
                .Select(s => new StudentDepartmentDto
                {
                    FullName = s.FullName,
                    Email = s.Email,
                    DepartmentName = s.Department.Name
                })
                .ToListAsync();

        }

        public async Task Update(int id , StudentUpdateDto std)
        {
            var student =  await _unitOfWork.Repository<Student>().GetByIdAsync(id);

            if (student == null)
                throw new NotFoundException();

            student.FullName = std.FullName;
            student.Email = std.Email;
            student.DateOfBirth = std.DateOfBirth;
            student.DepartmentId = std.DepartmentId;

            await _unitOfWork.SaveAsync();
        }

        public async Task<string> Delete(int id)
        {
            var student = await _unitOfWork.Repository<Student>().GetByIdAsync(id);
            if (student == null) throw new NotFoundException();
            _unitOfWork.Repository<Student>().Delete(student);
            await _unitOfWork.SaveAsync();

            return "Deleted successfully";

        }
    }
}
