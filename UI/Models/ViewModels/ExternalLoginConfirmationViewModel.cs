using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}