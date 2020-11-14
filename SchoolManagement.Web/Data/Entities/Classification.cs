using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Data.Entities
{
    public class Classification : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Classificação")]
        public int Score { get; set; }

        [Display(Name = "Faltas Justificadas")]
        public int JustifiedAbsence { get; set; }

        [Display(Name = "Faltas Injustificadas")]
        public int UnjustifiedAbsence { get; set; }

        public int StudentId { get; set; }

        public int DisciplineId { get; set; }

        public Student Student { get; set; }

        public Discipline Discipline { get; set; }
    }
}
