using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class ActorFilm
{
    public int ActorFilmId { get; set; }

    public int? ActorId { get; set; }

    public int? FilmId { get; set; }

    public int? CharacterId { get; set; }

    public string? ActorRol { get; set; }

    public virtual Actor? Actor { get; set; }

    public virtual Character? Character { get; set; }

    public virtual Film? Film { get; set; }
}
