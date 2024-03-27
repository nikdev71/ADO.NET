using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCamp.Model
{
    internal class Rating
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Movie Movie { get; set; }
        public double Grade {  get; set; }
    }
}
