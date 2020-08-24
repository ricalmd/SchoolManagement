using System.Collections.Generic;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Models
{
    public class CourseAndDisciplinesViewModel : Course
    {
        public ICollection<Discipline> Disciplines { get; set; }
    }
}
