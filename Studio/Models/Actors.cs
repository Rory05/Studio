using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studio.Models
{
    public class Actors
    {
        public Actors()
        {
            FilmActors = new List<FilmActors>();
        }
        public string Name { get; set; }
        public int Date { get; set; }
        public int FilmNumber { get; set; }
        public string FirstFilm { get; set; }
        public string Img { get; set; }
        public int Id { get; set; }
        public virtual ICollection<FilmActors> FilmActors { get; set; }
    }
}
