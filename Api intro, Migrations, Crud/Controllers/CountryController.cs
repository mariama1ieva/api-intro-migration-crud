using Api_intro__Migrations__Crud.Data;
using Api_intro__Migrations__Crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_intro__Migrations__Crud.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CountryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Country country)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            bool existCountry = await _context.Countries.AnyAsync(m => m.Name.Trim() == country.Name.Trim());
            if (existCountry)
            {
                ModelState.AddModelError("Name", "Country already exist");
                return Ok();
            }
            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), country);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Countries.Include(m => m.Cities).ToListAsync());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (id == null) return BadRequest();
            var existCountry = await _context.Countries.FindAsync(id);
            if (existCountry == null) return NotFound();

            _context.Countries.Remove(existCountry);
            await _context.SaveChangesAsync();
            return Ok();


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, Country newName)
        {
            if (id == null) return BadRequest();
            var existData = await _context.Countries.FindAsync(id);
            if (!ModelState.IsValid)
            {
                newName.Name = existData.Name;
                return Ok(newName);

            }
            if (existData == null) return NotFound();

            existData.Name = newName.Name;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
