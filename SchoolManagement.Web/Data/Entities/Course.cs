using Microsoft.CodeAnalysis.Operations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Data.Entities
{
    public class Course : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Designação")]
        [MaxLength(50, ErrorMessage = "O campo {0} só pode conter até {1} caracteres.")]
        public string Name { get; set; }

        [Required]
        public string QNQ { get; set; }

        [Required]
        public string QEQ { get; set; }

        [Display(Name = "Referência")]
        [Required]
        public string Reference { get; set; }

        [Display(Name = "Perfil Profissional")]
        [Required]
        public string Profile { get; set; }

        public ICollection<CourseWithSubjects> CoursesAndSubjects { get; set; }

        public User User { get; set; }
    }
}
