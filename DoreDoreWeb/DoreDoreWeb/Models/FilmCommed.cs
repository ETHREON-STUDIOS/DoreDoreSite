using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class FilmCommed
{
    public int FilmCommendsId { get; set; }

    public int? FilmId { get; set; }

    public int? CommentsUserId { get; set; }

    public virtual CommentsUser? CommentsUser { get; set; }

    public virtual Film? Film { get; set; }
}
