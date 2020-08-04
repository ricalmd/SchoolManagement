using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Data.Entities
{
    public class Subject : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Designação")]
        [MaxLength(50, ErrorMessage = "O campo {0} só pode conter até {1} caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Carga horária")]
        public int Workload { get; set; }

        [Display(Name = "Código")]
        [Required]
        public string Code { get; set; }

        [Display(Name = "Créditos")]
        [Required]
        public double Credit { get; set; }

        [Display(Name = "Objetivos")]
        [Required]
        [MaxLength(250, ErrorMessage = "O campo {0} só pode conter até {1} caracteres.")]
        public string Objectiv { get; set; }

        [Display(Name = "Conteúdos")]
        [Required]
        public string Content { get; set; }

        [Display(Name = "Referências")]
        [Required]
        public string Reference { get; set; }

        public User User { get; set; }
    }
}
