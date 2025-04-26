using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class FilmFragman
{
    public int FilmFragmanId { get; set; }

    public int? FilmId { get; set; }

    public int? FragmanId { get; set; }

    public virtual Film? Film { get; set; }

    public virtual Fragman? Fragman { get; set; }
}
