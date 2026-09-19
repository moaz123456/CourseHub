//using Microsoft.AspNetCore.Mvc;
//using UniversityManagement.API.Models;
//using UniversityManagement.API.UnitOfWork;

//namespace UniversityManagement.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class DepartmentsController : ControllerBase
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public DepartmentsController(IUnitOfWork unit)
//        {
//            _unitOfWork = unit;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var result = await _unitOfWork.Departments.GetAllAsync();

//            return Ok(result);
//        }

//        [HttpGet("deptsWithStds")]
//        public async Task<IActionResult> GetAllWithStudents()
//        {
//            var result = await _unitOfWork.Departments.GetAllWithStudents();

//            return Ok(result);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var result = await _unitOfWork.Departments.GetByIdAsync(id);

//            if (result == null)
//                return NotFound();

//            return Ok(result);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Add(Department department)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            _unitOfWork.Departments.Add(department);

//            await _unitOfWork.SaveAsync();

//            return CreatedAtAction(
//                nameof(GetById),
//                new { id = department.Id },
//                department
//            );
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(int id, Department newDept)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var dept = await _unitOfWork.Departments.GetByIdAsync(id);

//            if (dept == null)
//                return NotFound();

//            dept.Name = newDept.Name;

//            await _unitOfWork.SaveAsync();

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var dept = await _unitOfWork.Departments.GetByIdAsync(id);

//            if (dept == null)
//                return NotFound();

//            _unitOfWork.Departments.Delete(dept);

//            await _unitOfWork.SaveAsync();

//            return NoContent();
//        }
//    }
//}