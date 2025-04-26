using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class Actor
{
    public int ActorId { get; set; }

    public string? ActorName { get; set; }

    public string? ActorSurname { get; set; }

    public DateOnly? ActorDate { get; set; }

    public string? ActorFotoDosyaYolu { get; set; }

    public short? ActorAyakNo { get; set; }

    public int? ActorGander { get; set; }

    public virtual ICollection<ActorFilm> ActorFilms { get; set; } = new List<ActorFilm>();
}
