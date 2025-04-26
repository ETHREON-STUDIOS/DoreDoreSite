using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class Director
{
    public int DirectorId { get; set; }

    public string? DirectorName { get; set; }

    public string? DirectorSurName { get; set; }

    public DateOnly? DirectorDate { get; set; }

    public int? DirectorGender { get; set; }

    public virtual ICollection<DirectorFilm> DirectorFilms { get; set; } = new List<DirectorFilm>();
}
