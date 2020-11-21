using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O email é inválido")]
        [Display(Name = "Email")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MinLength(6, ErrorMessage = "A password deve conter no mínimo {1} caracteres")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public List<User> Users { get; set; }
    }
}
