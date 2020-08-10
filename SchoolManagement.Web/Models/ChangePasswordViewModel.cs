using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Palavra-passe atual")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "Nova palavra-passe")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        public string Confirm { get; set; }
    }
}
