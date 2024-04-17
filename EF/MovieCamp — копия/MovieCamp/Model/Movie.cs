using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCamp.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual Director Director { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public int Year { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public string Poster { get; set; }
        public string Description { get; set; }
    }
}
