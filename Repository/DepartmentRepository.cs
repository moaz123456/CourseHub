using UniversityManagement.API.Models;
using UniversityManagement.API.Context;
using UniversityManagement.API.Repository;
using UniversityManagement.API.Repository.Interfaces;
using UniversityManagement.API.DTOs.DepartmentsDTOs;
using UniversityManagement.API.DTOs.StudentDTOs;
using Microsoft.EntityFrameworkCore;
namespace UniversityManagement.API.Repository
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly UniversityDbContext _context;
        public DepartmentRepository(UniversityDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<DepartmentsWithStudentsDto>> GetAllWithStudents()
        {
            return await _context.Departments
                .Select(d => new DepartmentsWithStudentsDto
                {
                    Id = d.Id,
                    DepartmentName = d.Name,

                    Students = d.Students.Select(s => new StudentDepartmentDto
                    {
                        FullName = s.FullName,
                        Email = s.Email
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}
