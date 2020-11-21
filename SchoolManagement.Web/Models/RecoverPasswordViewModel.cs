using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O email é inválido")]
        public string Email { get; set; }
    }
}
