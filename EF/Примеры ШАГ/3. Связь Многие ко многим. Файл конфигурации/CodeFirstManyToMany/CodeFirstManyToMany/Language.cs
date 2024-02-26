using System.Collections.Generic;

namespace CodeFirstManyToMany
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Continent> Continents { get; set; }
    }
}