using System;
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

        [Display(Name = "Duração")]
        [DisplayFormat(DataFormatString ="{0:HH}", ApplyFormatInEditMode = false)]
        public DateTime Duration { get; set; }

        [Display(Name = "Código")]
        [Required]
        public string Code { get; set; }

        [Display(Name = "Área")]
        [Required]
        public string Field { get; set; }

        [Display(Name = "Qualificação")]
        [Required]
        public string Qualification { get; set; }

        [Display(Name = "Créditos")]
        [Required]
        public int Credit { get; set; }

        [Display(Name = "Descrição")]
        [Required]
        [MaxLength(250, ErrorMessage = "O campo {0} só pode conter até {1} caracteres.")]
        public string Description { get; set; }

        [Display(Name = "Nível")]
        [Required]
        public string Level { get; set; }

        public User User { get; set; }
    }
}
