using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class Film
{
    public int FilmId { get; set; }

    public string? FilmName { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public double? FilmImdb { get; set; }

    public string? FilmDescriptiyon { get; set; }

    public double? FilmDuratiyon { get; set; }

    public string? FilmDosyaYolu { get; set; }

    public string? FilmImageDosyaYolu { get; set; }

    public virtual ICollection<ActorFilm> ActorFilms { get; set; } = new List<ActorFilm>();

    public virtual ICollection<AfisFilmId> AfisFilmIds { get; set; } = new List<AfisFilmId>();

    public virtual ICollection<DirectorFilm> DirectorFilms { get; set; } = new List<DirectorFilm>();

    public virtual ICollection<FilmCommed> FilmCommeds { get; set; } = new List<FilmCommed>();

    public virtual ICollection<FilmFragman> FilmFragmen { get; set; } = new List<FilmFragman>();

    public virtual ICollection<TypeFilm> TypeFilms { get; set; } = new List<TypeFilm>();
}
