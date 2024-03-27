using System;
using System.Collections.Generic;

namespace test;

public partial class GenresMovie
{
    public int GenreId { get; set; }

    public int MovieId { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
