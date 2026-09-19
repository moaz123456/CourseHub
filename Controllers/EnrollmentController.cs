using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;
using UniversityManagement.API.Context;
using UniversityManagement.API.DTOs.EnrollmentsDTOs;
using UniversityManagement.API.Models;

namespace UniversityManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly UniversityDbContext _context;
        public EnrollmentController(UniversityDbContext dbContext)
        {
            _context = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Enrollments
            //.Include(e => e.Student)
            //.Include(e => e.Course)
            .Select(e => new
            {
                e.StudentId,
                StudentName = e.Student.FullName,
                e.CourseId,
                e.Course.Name,
                e.Grade,
                e.EnrollmentDate

            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("{studnetid}/{courseid}")]
        public async Task<IActionResult> GetById(int studnetid, int courseid)
        {
            var result = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.StudentId == studnetid && e.CourseId == courseid);

            if (result == null) return NotFound();

            EnrollmentStudentCourseDTO studentCourseDTO = new EnrollmentStudentCourseDTO
            {
                StudentName = result.Student.FullName,
                CourseName = result.Course.Name,
                Grade = result.Grade,
                EnrollmentDate = result.EnrollmentDate

            };
            return Ok(studentCourseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EnrollmentCreateDto EnrolCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var checkStudent = await _context.Students
                .FindAsync(EnrolCreate.StudentId);

            if (checkStudent == null)
                return NotFound("Student not found.");

            var checkCourse = await _context.Courses
                .FindAsync(EnrolCreate.CourseId);

            if (checkCourse == null)
                return NotFound("Course not found.");

            var checkAll = await _context.Enrollments
                .FindAsync([EnrolCreate.StudentId, EnrolCreate.CourseId]);

            if (checkAll != null)
                return Conflict("Student is already enrolled in this course.");

            var enroll = new Enrollment
            {
                StudentId = EnrolCreate.StudentId,
                CourseId = EnrolCreate.CourseId,
                Grade = EnrolCreate.Grade,
                EnrollmentDate = EnrolCreate.EnrollmentDate
            };

            _context.Enrollments.Add(enroll);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    studnetid = enroll.StudentId,
                    courseid = enroll.CourseId
                },
                enroll);
        }

        [HttpPut("{StudentId}/{CourseId}")]

        public async Task<IActionResult> Update(int StudentId ,int CourseId , EnrollmentUpdateDto enrollmentUpdateDTO)
        {
            var checkall = await _context.Enrollments.FindAsync([StudentId,CourseId]);
            if (checkall == null || !ModelState.IsValid) return BadRequest();

            checkall.EnrollmentDate = enrollmentUpdateDTO.EnrollmentDate;
            checkall.Grade = enrollmentUpdateDTO.Grade;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{StudentId}/{CourseId}")]
        public async Task<IActionResult> Delete(int StudentId , int CourseId)
        {
            var result = await _context.Enrollments.FindAsync([StudentId,CourseId]);
            if (result == null) return NotFound();

             _context.Enrollments.Remove(result);
             await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
