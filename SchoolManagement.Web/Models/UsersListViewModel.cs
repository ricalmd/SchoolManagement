using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Models
{
    public class UsersListViewModel
    {
        [Display(Name = "Nome do utilizador")]
        public string Name { get; set; }

        [Display(Name = "Tipo de utilizador")]
        public string UserType { get; set; }

        public string Email { get; set; }

        [Display(Name = "Confirmação da conta")]
        public bool Confirmed { get; set; }
    }
}
