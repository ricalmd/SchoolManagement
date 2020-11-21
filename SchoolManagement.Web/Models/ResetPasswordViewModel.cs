using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "O email é inválido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A password deve conter no mínimo {1} caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirmar password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
