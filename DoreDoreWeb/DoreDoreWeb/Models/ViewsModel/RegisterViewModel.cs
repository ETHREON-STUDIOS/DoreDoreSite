namespace DoreDoreWeb.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string? UserName { get; set; }
        public string? UserEposta { get; set; }
        public string? UserPassword { get; set; }
        public DateOnly? BirthDate { get; set; }
        public bool? Gender { get; set; }
    }
}
