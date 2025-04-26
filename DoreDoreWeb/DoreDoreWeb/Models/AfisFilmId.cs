using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class AfisFilmId
{
    public int AfisFilmId1 { get; set; }

    public int? AfisId { get; set; }

    public int? FilmId { get; set; }

    public virtual Afi? Afis { get; set; }

    public virtual Film? Film { get; set; }
}
