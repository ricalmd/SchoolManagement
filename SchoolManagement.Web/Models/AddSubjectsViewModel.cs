using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Models
{
    public class AddSubjectsViewModel : Course
    {
        [Range(1, int.MaxValue, ErrorMessage = "Deve selecionar uma disciplina")]
        public int SubjectId { get; set; }

        public IEnumerable<SelectListItem> Subject { get; set; }
    }
}
