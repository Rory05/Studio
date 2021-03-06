﻿using System;
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
    public class FilmsController : ControllerBase
    {
        private readonly StudioContext _context;

        public FilmsController(StudioContext context)
        {
            _context = context;
        }

        // GET: api/Films
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Films>>> GetFilms(string genreName, string actorName)
        {
            var films = _context.Films.Select(f => new ExpandedFilms(f, _context));
            if (genreName != null)
            {
                string sql = "select Films.* " +
                                "from Films inner join (FilmGenres inner join Genres on Genres.Id = FilmGenres.GenresId) on Films.Id = FilmGenres.FilmsId " +
                                "where Genres.Name = '" + genreName + "'";
                var film = _context.Films.FromSqlRaw(sql).Select(f => new ExpandedFilms(f, _context));
                return await film.ToListAsync();
            }
            else
            {
                if (actorName != null)
                {
                    string sql = "select Films.* " +
                                "from Films inner join (FilmActors inner join Actors on Actors.Id = FilmActors.ActorsId) on Films.Id = FilmActors.FilmsId " +
                                "where Actors.Name = '" + actorName + "'";
                    var film = _context.Films.FromSqlRaw(sql).Select(f => new ExpandedFilms(f, _context));
                    return await film.ToListAsync();
                }
            }
            return await films.ToListAsync();
        }

        // GET: api/Films/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Films>> GetFilms(int id)
        {

            var filmgenres = await _context.FilmGenres.Where(b => b.GenresId == id).FirstOrDefaultAsync();
            var films = await _context.Films.Where(b => b.Id == filmgenres.FilmsId).FirstAsync();

            return films;
        }

        // PUT: api/Films/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilms(int id, Films films)
        {
            if (id != films.Id)
            {
                return BadRequest();
            }

            _context.Entry(films).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmsExists(id))
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

        // POST: api/Films
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Films>> PostFilms(Films films)
        {
            _context.Films.Add(films);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilms", new { id = films.Id }, films);
        }

        // DELETE: api/Films/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Films>> DeleteFilms(int id)
        {
            var films = await _context.Films.FindAsync(id);
            if (films == null)
            {
                return NotFound();
            }

            _context.Films.Remove(films);
            await _context.SaveChangesAsync();

            return films;
        }

        private bool FilmsExists(int id)
        {
            return _context.Films.Any(e => e.Id == id);
        }

        [HttpOptions("{id}")]
        public async Task<ActionResult<Films>> OptionsFilms(int id)
        {
            
            var filmgenres = await _context.FilmGenres.Where(b => b.GenresId == id).FirstOrDefaultAsync();
            var films = await _context.Films.Where(b => b.Id == filmgenres.FilmsId).FirstAsync();

            return films;
        }
    }
    public class ExpandedFilms : Films
    {
        public List<string> Genres { get; set; }
        public List<string> Actors { get; set; }
        public ExpandedFilms(Films films, StudioContext context)
        {
            Name = films.Name;
            Duration = films.Duration;
            Year = films.Year;
            Age = films.Age;
            Description = films.Description;
            Img = films.Img;
            Id = films.Id;
            CountryId = films.CountryId;
            string sql = "select Genres.* " +
                         "from Films inner join (FilmGenres inner join Genres on Genres.Id = FilmGenres.GenresId) on Films.Id = FilmGenres.FilmsId " +
                         "where Films.Name = '" + Name + "'";
            Genres = context.Genres.FromSqlRaw(sql).Select(g => g.Name).ToList();
            string sql2 = "select Actors.* " +
                         "from Films inner join (FilmActors inner join Actors on Actors.Id = FilmActors.ActorsId) on Films.Id = FilmActors.FilmsId " +
                         "where Films.Name = '" + Name + "'";
            Actors = context.Actors.FromSqlRaw(sql2).Select(g => g.Name).ToList();
        }
    }
}
