﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCamp.Model
{
    public class Genre
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
