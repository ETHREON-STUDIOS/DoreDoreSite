using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class User
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? UserEposta { get; set; }

    public string? UserPassword { get; set; }

    public bool? UserLv { get; set; }

    public DateOnly? BirthDate { get; set; }

    public bool? Gender { get; set; }

    public string? ProfilePicture { get; set; }

    public virtual ICollection<CommentsUser> CommentsUsers { get; set; } = new List<CommentsUser>();
}
