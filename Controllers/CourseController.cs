using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityManagement.API.Context;
using UniversityManagement.API.DTOs.CourseDTOs;
using UniversityManagement.API.Models;

namespace UniversityManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly UniversityDbContext _context;

        public CourseController(UniversityDbContext context)
        {
            _context = context;
        }

        // GET: api/Course
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Courses.ToListAsync();

            return Ok(result);
        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/Course
        [HttpPost]
        public async Task<IActionResult> Add(CourseCreateDTO course)
        {
            if (course == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var crs = new Course
            {
                Name = course.Name,
                Credits = course.Credits,
            };
            _context.Courses.Add(crs);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = crs.Id },
                crs);
        }

        // PUT: api/Course/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Course newCourse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _context.Courses.FindAsync(id);

            if (result == null)
                return NotFound();

            result.Name = newCourse.Name;
            result.Credits = newCourse.Credits;

            await _context.SaveChangesAsync();

            return Ok(result);
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Courses.FindAsync(id);

            if (result == null)
                return NotFound();

            _context.Courses.Remove(result);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("credits")]
        public async Task<IActionResult> GetCourseMoreThan3Credits()
        {
            var result = await _context.Courses.Where(c => c.Credits > 3).ToListAsync();
            return Ok(result);
        }
    }
}