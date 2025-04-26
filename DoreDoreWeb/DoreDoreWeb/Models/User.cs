using System;
using System.Collections.Generic;

namespace DoreDoreWeb.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserEposta { get; set; }

    public string? UserPassaworld { get; set; }

    public bool? UserLv { get; set; }

    public DateOnly? BirthDate { get; set; }

    public bool? Gender { get; set; }

    public string? ProfilFoto { get; set; }

    public virtual ICollection<CommentsUser> CommentsUsers { get; set; } = new List<CommentsUser>();
}
