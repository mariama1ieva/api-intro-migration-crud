using Api_intro__Migrations__Crud.Data;
using Api_intro__Migrations__Crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_intro__Migrations__Crud.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CityController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] City city, int countryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool existCity = await _context.Cities.AnyAsync(m => m.Name.Trim() == city.Name.Trim());
            if (existCity)
            {
                ModelState.AddModelError("Name", "City already exists");
                return BadRequest(ModelState);
            }


            var existingCity = await _context.Countries.FindAsync(countryId);
            if (existingCity == null && city.Id != countryId)
            {
                ModelState.AddModelError("CountryId", "Invalid country id");
                return BadRequest(ModelState);
            }

            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), city);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (id == null) return BadRequest();
            var existCity = await _context.Cities.FindAsync(id);
            if (existCity == null) return NotFound();

            _context.Cities.Remove(existCity);
            await _context.SaveChangesAsync();
            return Ok();


        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Cities.ToListAsync());
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, City newData)
        {
            if (id == null)
                return BadRequest();

            var existingCity = await _context.Cities.FindAsync(id);
            if (existingCity == null)
                return NotFound();

            existingCity.Name = newData.Name;

            if (newData.CountryId != 0)
            {
                var existingCountry = await _context.Countries.FindAsync(newData.CountryId);
                if (existingCountry == null)
                {
                    ModelState.AddModelError("CountryId", "Invalid city ID");
                    return BadRequest(ModelState);
                }


                existingCity.CountryId = newData.CountryId;
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
