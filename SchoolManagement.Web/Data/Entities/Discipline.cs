using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Data.Entities
{
    public class Discipline : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Designação")]
        [MaxLength(50, ErrorMessage = "O campo {0} só pode conter até {1} caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Carga horária")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Workload { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Code { get; set; }

        [Display(Name = "Créditos")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public double Credit { get; set; }

        [Display(Name = "Objetivos")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Objectiv { get; set; }

        [Display(Name = "Conteúdos")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Content { get; set; }

        public User User { get; set; }
    }
}
