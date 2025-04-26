using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class Type
{
    public int TypeId { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<TypeFilm> TypeFilms { get; set; } = new List<TypeFilm>();
}
