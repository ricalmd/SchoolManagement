using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Data.Entities
{
    public class Class : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Designação")]
        [MaxLength(50, ErrorMessage = "O campo {0} só pode conter até {1} caracteres.")]
        public string NameClass { get; set; }

        [Display(Name = "Início")]
        public DateTime BeginSchedule { get; set; }

        [Display(Name = "Fim")]
        public DateTime EndSchedule { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public User User { get; set; }
    }
}
