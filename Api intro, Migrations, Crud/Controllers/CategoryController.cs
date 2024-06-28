
using Api_intro__Migrations__Crud.Data;
using Api_intro__Migrations__Crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_intro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Category category)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), category);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Categories.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var existData = await _context.Categories.FindAsync(id);
            if (existData == null) return NotFound();
            return Ok(existData);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (id == null) return BadRequest();
            var existData = await _context.Categories.FindAsync(id);
            if (existData == null) return NotFound();

            _context.Categories.Remove(existData);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Edit([FromQuery] int id, Category newName)
        {
            if (id == null) return BadRequest();
            Category existData = await _context.Categories.FindAsync(id);
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
