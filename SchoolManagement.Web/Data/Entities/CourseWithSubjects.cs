using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Data.Entities
{
    public class CourseWithSubjects : IEntity
    {
        public int Id { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
}
