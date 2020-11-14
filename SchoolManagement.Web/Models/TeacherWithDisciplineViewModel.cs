using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Models
{
    public class TeacherWithDisciplineViewModel
    {
        [Display(Name = "ID")]
        public int TeacherId { get; set; }

        [Display(Name = "Nome")]
        public string UserName { get; set; }

        [Display(Name = "Disciplina")]
        public string DisciplineName { get; set; }
    }
}
