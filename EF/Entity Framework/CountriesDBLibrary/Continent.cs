using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Framework
{
    public class Continent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Country> Countries { get; set; }

    }
}
