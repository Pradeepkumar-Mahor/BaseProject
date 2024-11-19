namespace UI.Models.IdenityUserAccess
{
    public class PermissionViewModel
    {
        public required string RoleId { get; set; }
        public required IList<RoleClaimsViewModel> RoleClaims { get; set; }
    }

    public class RoleClaimsViewModel
    {
        public required string Type { get; set; }
        public required string Value { get; set; }
        public bool Selected { get; set; }
    }
}