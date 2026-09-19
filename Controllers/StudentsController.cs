using Microsoft.AspNetCore.Mvc;
using UniversityManagement.API.DTOs;
using UniversityManagement.API.DTOs.StudentDTOs;
using UniversityManagement.API.IServices;
using UniversityManagement.API.Services;
using UniversityManagement.API.Models;
using UniversityManagement.API.UnitOfWork;

namespace UniversityManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService service;

        public StudentsController(IStudentService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetStudentDetails(int id)
        {
            var result = await service.GetStudentDetailsAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudentCreateDto std)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await service.AddStudent(std);

            return CreatedAtAction(
                nameof(GetStudentDetails),
                new { id = result.Id },
                result
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,StudentUpdateDto std)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await service.Update(id, std);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await service.Delete(id);

            return Ok(result);
        }

        [HttpGet("department/{deptId}")]
        public async Task<IActionResult> StudentsByDepartment(int deptId)
        {
            var students = await service
                .GetStudentsByDepartmentAsync(deptId);

            return Ok(students);
        }
    }
}