namespace DoreDoreWeb.Models.ViewsModel
{
    public class Users
    {
        public int Id { get; set; }
        public string? UserName { get; set; }  // nullable
        public string? UserEposta { get; set; }  // nullable
        public string? UserPassword { get; set; }  // nullable
        public DateOnly? BirthDate { get; set; }
        public bool? Gender { get; set; }
        public string? ProfilFoto { get; set; }  // nullable
    }

}
