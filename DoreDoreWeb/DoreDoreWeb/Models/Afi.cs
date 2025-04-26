using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class Afi
{
    public int AfisId { get; set; }

    public string? AfisName { get; set; }

    public DateOnly? AfisDate { get; set; }

    public string? AfisDosyaYolu { get; set; }

    public virtual ICollection<AfisFilmId> AfisFilmIds { get; set; } = new List<AfisFilmId>();
}
