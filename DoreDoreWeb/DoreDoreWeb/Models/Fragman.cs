using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class Fragman
{
    public int FragmanId { get; set; }

    public string? FilmFragmanYol { get; set; }

    public DateOnly? FragmanDate { get; set; }

    public string? FragmanDescription { get; set; }

    public virtual ICollection<FilmFragman> FilmFragmen { get; set; } = new List<FilmFragman>();
}
