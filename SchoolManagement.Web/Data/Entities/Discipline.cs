using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Data.Entities
{
    public class Discipline : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Designação")]
        [MaxLength(50, ErrorMessage = "O campo {0} só pode conter até {1} caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Carga horária")]
        [Required]
        public int Workload { get; set; }

        [Display(Name = "Código")]
        [Required]
        public string Code { get; set; }

        [Display(Name = "Créditos")]
        [Required]
        public double Credit { get; set; }

        [Display(Name = "Objetivos")]
        [Required]
        public string Objectiv { get; set; }

        [Display(Name = "Conteúdos")]
        [Required]
        public string Content { get; set; }

        public User User { get; set; }
    }
}
