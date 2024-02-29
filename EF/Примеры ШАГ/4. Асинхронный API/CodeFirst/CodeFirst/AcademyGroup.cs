﻿using System.Collections.Generic;

namespace CodeFirst
{
    public class AcademyGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
