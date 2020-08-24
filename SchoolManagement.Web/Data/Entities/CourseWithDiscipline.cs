using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Data.Entities
{
    public class CourseWithDiscipline : IEntity
    {
        public int Id { get; set; }

        [Required]
        public int DisciplineId { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
}
