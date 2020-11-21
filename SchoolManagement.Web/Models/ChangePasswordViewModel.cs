using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Palavra-passe atual")]
        [MinLength(6, ErrorMessage = "A password deve conter no mínimo {1} caracteres")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Nova palavra-passe")]
        [MinLength(6, ErrorMessage = "A password deve conter no mínimo {1} caracteres")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "O campo Confirmar é obrigatório")]
        [Compare("NewPassword")]
        [Display(Name = "Confirmar nova palavra-passe")]
        public string Confirm { get; set; }
    }
}
