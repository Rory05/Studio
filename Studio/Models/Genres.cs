using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studio.Models
{
    public class Genres
    {
        public Genres()
        {
            FilmGenres = new List<FilmGenres>();
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public virtual ICollection<FilmGenres> FilmGenres { get; set; }
    }
}
