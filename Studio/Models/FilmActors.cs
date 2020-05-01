using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studio.Models
{
    public class FilmActors
    {
        //public int FilmsId { get; set; }
        //public int ActorsId { get; set; }
        public int Id { get; set; }
        public virtual Films Films { get; set; }
        public virtual Actors Actors { get; set; }
    }
}
