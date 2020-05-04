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
    public class FilmGenresController : ControllerBase
    {
        private readonly StudioContext _context;

        public FilmGenresController(StudioContext context)
        {
            _context = context;
        }

        // GET: api/FilmGenres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmGenres>>> GetFilmGenres()
        {
            return await _context.FilmGenres.ToListAsync();
        }

        // GET: api/FilmGenres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmGenres>> GetFilmGenres(int id)
        {
            var filmGenres = await _context.FilmGenres.FindAsync(id);

            if (filmGenres == null)
            {
                return NotFound();
            }

            return filmGenres;
        }

        // PUT: api/FilmGenres/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilmGenres(int id, FilmGenres filmGenres)
        {
            if (id != filmGenres.Id)
            {
                return BadRequest();
            }

            _context.Entry(filmGenres).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmGenresExists(id))
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

        // POST: api/FilmGenres
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FilmGenres>> PostFilmGenres(FilmGenres filmGenres)
        {
            _context.FilmGenres.Add(filmGenres);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilmGenres", new { id = filmGenres.Id }, filmGenres);
        }

        // DELETE: api/FilmGenres/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FilmGenres>> DeleteFilmGenres(int id)
        {
            var filmGenres = await _context.FilmGenres.FindAsync(id);
            if (filmGenres == null)
            {
                return NotFound();
            }

            _context.FilmGenres.Remove(filmGenres);
            await _context.SaveChangesAsync();

            return filmGenres;
        }

        private bool FilmGenresExists(int id)
        {
            return _context.FilmGenres.Any(e => e.Id == id);
        }
    }
}
