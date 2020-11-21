using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Data.Entities
{
    public class Course : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Designação")]
        [MaxLength(50, ErrorMessage = "O campo {0} só pode conter até {1} caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string QNQ { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string QEQ { get; set; }

        [Display(Name = "Referência")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Reference { get; set; }

        [Display(Name = "Perfil Profissional")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Profile { get; set; }

        public User User { get; set; }
    }
}