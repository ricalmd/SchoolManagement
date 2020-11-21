using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolManagement.Web.Models
{
    public class ChangeElementViewModel
    {
        [Display(Name = "Lista de emails (formadores)")]
        public string EmailTeacherId { get; set; }

        public IEnumerable<SelectListItem> EmailTeachers { get; set; }

        [Display(Name = "Lista de emails (estudantes)")]
        public string EmailStudentId { get; set; }

        public IEnumerable<SelectListItem> EmailStudents { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Tipo de Utilizador")]
        public string Status { get; set; }

        [Display(Name = "Lista de turmas")]
        public int ClassId { get; set; }

        public IEnumerable<SelectListItem> Class { get; set; }

        [Display(Name = "Lista de disciplinas")]
        public int DisciplineId { get; set; }

        public IEnumerable<SelectListItem> Discipline { get; set; }
    }
}
