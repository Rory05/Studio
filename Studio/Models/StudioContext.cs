using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Studio.Models
{
     public class StudioContext : DbContext
    {
        public virtual DbSet<Films> Films { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<FilmGenres> FilmGenres { get; set; }
        public virtual DbSet<FilmActors> FilmActors { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Actors> Actors { get; set; }

        public StudioContext(DbContextOptions<StudioContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
