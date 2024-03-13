using System.Collections.Generic;

namespace CodeFirstManyToMany
{
    public class Continent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Language> Languages { get; set; }
    }
}