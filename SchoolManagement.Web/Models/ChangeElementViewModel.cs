using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolManagement.Web.Models
{
    public class ChangeElementViewModel
    {
        public string EmailId { get; set; }

        public IEnumerable<SelectListItem> Email { get; set; }

        [Required]
        [Display(Name = "Tipo de Utilizador")]
        public string Status { get; set; }

        public int ClassId { get; set; }

        [Display(Name = "Lista de turmas")]
        public IEnumerable<SelectListItem> Class { get; set; }

        public int DisciplineId { get; set; }

        [Display(Name = "Lista de disciplinas")]
        public IEnumerable<SelectListItem> Discipline { get; set; }
    }
}
