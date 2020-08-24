using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Models
{
    public class AddDisciplinesViewModel : Course
    {
        public int DisciplineId { get; set; }

        public IEnumerable<SelectListItem> Discipline { get; set; }
    }
}