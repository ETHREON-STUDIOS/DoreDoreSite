using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class Comment
{
    public int CommetId { get; set; }

    public string? CommentText { get; set; }

    public DateOnly? CommentDate { get; set; }

    public int? CommentLikes { get; set; }

    public virtual ICollection<CommentsUser> CommentsUsers { get; set; } = new List<CommentsUser>();
}
