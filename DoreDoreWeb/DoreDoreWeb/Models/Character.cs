using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class Character
{
    public int CharacterId { get; set; }

    public string? CharacterName { get; set; }

    public string? CharacterSorName { get; set; }

    public DateOnly? CharacterBDate { get; set; }

    public bool? CharacterGender { get; set; }

    public virtual ICollection<ActorFilm> ActorFilms { get; set; } = new List<ActorFilm>();
}
