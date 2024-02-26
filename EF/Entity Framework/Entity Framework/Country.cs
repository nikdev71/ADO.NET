using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Framework
{
    public class Country
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Capital { get; set; }
        public double Population { get; set; }
        public long Square { get; set; }
        public virtual Continent Continent { get; set; }

    }
}
