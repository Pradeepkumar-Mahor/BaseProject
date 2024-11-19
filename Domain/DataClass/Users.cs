using Microsoft.AspNetCore.Identity;

namespace Domain.DataClass
{
    public class ApplicationUsers : IdentityUser
    {

        public string FirstName { get; set; } = string.Empty;
        public string MidName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateOnly Dob { get; set; }
        public string AadhaarNo { get; set; } = string.Empty;
        public string PanNo { get; set; } = string.Empty;

        public string Photo { get; set; } = string.Empty;

        public DateTime? CreateOnDate { get; set; }
    }
}