using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class TypeFilm
{
    public int TypeFilmId { get; set; }

    public int? TypeId { get; set; }

    public int? FilmId { get; set; }

    public virtual Film? Film { get; set; }

    public virtual Type? Type { get; set; }
}
