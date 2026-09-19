using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityManagement.API.Context;
using UniversityManagement.API.Models;

namespace UniversityManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly UniversityDbContext _context;

        public AddressController(UniversityDbContext context)
        {
            _context = context;
        }

        // GET: api/Address
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Addresses.ToListAsync();

            return Ok(result);
        }

        // GET: api/Address/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.Addresses
                .FirstOrDefaultAsync(a => a.Id == id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/Address
        [HttpPost]
        public async Task<IActionResult> Add(Address address)
        {
            if (address == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Addresses.Add(address);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = address.Id },
                address);
        }

        // PUT: api/Address/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Address newAddress)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _context.Addresses.FindAsync(id);

            if (result == null)
                return NotFound();

            result.City = newAddress.City;
            result.Street = newAddress.Street;
            result.StudentId = newAddress.StudentId;

            await _context.SaveChangesAsync();

            return Ok(result);
        }

        // DELETE: api/Address/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Addresses.FindAsync(id);

            if (result == null)
                return NotFound();

            _context.Addresses.Remove(result);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}