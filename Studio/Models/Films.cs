using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studio.Models
{
    public class Films
    {
        public Films()
        {
            FilmGenres = new List<FilmGenres>();
            FilmActors = new List<FilmActors>();
        }
        public string Name { get; set; }
        public float Duration { get; set; }
        public int Year { get; set; }
        public int Age { get; set; }
        public int Id { get; set; }
        public int CountryId { get; set; }
        public virtual Countries Country { get; set; }

        public virtual ICollection<FilmGenres> FilmGenres { get; set; }
        public virtual ICollection<FilmActors> FilmActors { get; set; }
    }
}
