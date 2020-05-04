using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studio.Models;

namespace Studio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmActorsController : ControllerBase
    {
        private readonly StudioContext _context;

        public FilmActorsController(StudioContext context)
        {
            _context = context;
        }

        // GET: api/FilmActors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmActors>>> GetFilmActors()
        {
            return await _context.FilmActors.ToListAsync();
        }

        // GET: api/FilmActors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmActors>> GetFilmActors(int id)
        {
            var filmActors = await _context.FilmActors.FindAsync(id);

            if (filmActors == null)
            {
                return NotFound();
            }

            return filmActors;
        }

        // PUT: api/FilmActors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilmActors(int id, FilmActors filmActors)
        {
            if (id != filmActors.Id)
            {
                return BadRequest();
            }

            _context.Entry(filmActors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmActorsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FilmActors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FilmActors>> PostFilmActors(FilmActors filmActors)
        {
            _context.FilmActors.Add(filmActors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilmActors", new { id = filmActors.Id }, filmActors);
        }

        // DELETE: api/FilmActors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FilmActors>> DeleteFilmActors(int id)
        {
            var filmActors = await _context.FilmActors.FindAsync(id);
            if (filmActors == null)
            {
                return NotFound();
            }

            _context.FilmActors.Remove(filmActors);
            await _context.SaveChangesAsync();

            return filmActors;
        }

        private bool FilmActorsExists(int id)
        {
            return _context.FilmActors.Any(e => e.Id == id);
        }
    }
}
