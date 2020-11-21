using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Models
{
    public class RegisterNewUserViewModel : User
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O email é inválido")]
        [Display(Name = "Email")]
        public string Username { get; set; }

        public int ClassId { get; set; }

        [Display(Name = "Lista de turmas")]
        public IEnumerable<SelectListItem> Class { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Fotografia")]
        public IFormFile Photo { get; set; }
    }
}
