using System;
using System.Collections.Generic;

namespace SQL_в_Entity_Framework_Core;

public partial class Book
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Pages { get; set; }

    public int? YearPress { get; set; }

    public string? Themes { get; set; }

    public string? Author { get; set; }

    public string? Press { get; set; }

    public string? Comment { get; set; }

    public int? Quantity { get; set; }
}
