using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<SelectListItem>? RoleList { get; set; }

        [Display(Name = "Role")]
        public string RoleSelected { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string MidName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public DateOnly Dob { get; set; }

        [Required]
        public string AadhaarNo { get; set; } = string.Empty;

        [Required]
        public string PanNo { get; set; } = string.Empty;

        public string Photo { get; set; } = string.Empty;

        public DateTime? CreateOnDate { get; set; }
    }
}