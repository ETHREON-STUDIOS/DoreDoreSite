using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class DirectorFilm
{
    public int DirectorFilmId { get; set; }

    public int? DirectorId { get; set; }

    public int? FilmId { get; set; }

    public virtual Director? Director { get; set; }

    public virtual Film? Film { get; set; }
}
