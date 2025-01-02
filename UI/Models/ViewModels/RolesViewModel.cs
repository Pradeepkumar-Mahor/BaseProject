using Domain.DataClass;

namespace UI.Models
{
    public class RolesViewModel
    {
        public RolesViewModel()
        {
            RolesList = [];
        }

        public ApplicationUsers User { get; set; }
        public List<RoleSelection> RolesList { get; set; }
    }

    public class RoleSelection
    {
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}