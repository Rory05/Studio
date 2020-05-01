using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studio.Models
{
    public class Countries
    {
        public Countries()
        {
            Films = new List<Films>();
        }
        public string Name { get; set; }
        public string Studio { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Films> Films { get; set; }
    }
}
