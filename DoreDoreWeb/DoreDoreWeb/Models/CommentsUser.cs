using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class CommentsUser
{
    public int CommetUserId { get; set; }

    public int? CommentId { get; set; }

    public int? UserId { get; set; }

    public virtual Comment? Comment { get; set; }

    public virtual ICollection<FilmCommed> FilmCommeds { get; set; } = new List<FilmCommed>();

    public virtual User? User { get; set; }
}
