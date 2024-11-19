namespace UI.Models.IdenityUserAccess
{
    public class ManageUserRolesViewModel
    {
        public required string UserId { get; set; }
        public required string Email { get; set; }
        public required IList<UserRolesViewModel> UserRoles { get; set; }
    }

    public class UserRolesViewModel
    {
        public required string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}