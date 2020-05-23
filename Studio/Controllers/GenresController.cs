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
    public class GenresController : ControllerBase
    {
        private readonly StudioContext _context;

        public GenresController(StudioContext context)
        {
            _context = context;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genres>>> GetGenres(int id, string shoto)
        {
            var genres = _context.Genres;
            if (id != 0)
            {
                string sql = "select Genres.* " +
                            "from Genres inner join (FilmGenres inner join Films on Films.Id = FilmGenres.FilmsId) on Films.Id = FilmGenres.FilmsId " +
                            "where Films.Id = '" + id + "'";
                var genre = _context.Genres.FromSqlRaw(sql);
                return await genre.ToListAsync();
            }
            return await genres.ToListAsync();
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genres>> GetGenres(int id)
        {
            //var fg = _context.FilmGenres.Where(b => b.FilmsId == id).FirstOrDefaultAsync();
            //var g = genres.Where(b=> b.Id == fg.)



            //var filmgenre = from fg in _context.FilmGenres
            //                where fg.FilmsId == id
            //                select fg;
            //List<Genres> genres = new List<Genres>();
            //foreach (var fg in filmgenre)
            //{
            //    var gen = from g in _context.Genres
            //              where g.Id == fg.GenresId
            //              select g;
            //    foreach (var gn in gen)
            //    { genres.Add(gn); }
            


            var genres = await _context.Genres.FindAsync(id);

            if (genres == null)
            {
                return NotFound();
            }

            return genres;
        }

        // PUT: api/Genres/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenres(int id, Genres genres)
        {
            if (id != genres.Id)
            {
                return BadRequest();
            }

            _context.Entry(genres).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenresExists(id))
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

        // POST: api/Genres
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Genres>> PostGenres(Genres genres)
        {
            _context.Genres.Add(genres);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenres", new { id = genres.Id }, genres);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Genres>> DeleteGenres(int id)
        {
            var genres = await _context.Genres.FindAsync(id);
            if (genres == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genres);
            await _context.SaveChangesAsync();

            return genres;
        }

        private bool GenresExists(int id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}
