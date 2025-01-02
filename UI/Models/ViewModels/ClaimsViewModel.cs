using Domain.DataClass;

namespace UI.Models
{
    public class ClaimsViewModel
    {
        public ClaimsViewModel()
        {
            ClaimList = [];
        }

        public ApplicationUsers User { get; set; }
        public List<ClaimSelection> ClaimList { get; set; }
    }

    public class ClaimSelection
    {
        public string ClaimType { get; set; }
        public bool IsSelected { get; set; }
    }
}