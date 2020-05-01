using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studio.Models
{
    public class FilmGenres
    {
        //public int FilmsId { get; set; }
        //public int GenresId { get; set; }
        public int Id { get; set; }
        public virtual Films Films { get; set; }
        public virtual Genres Genres { get; set; }

    }
}
