using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Data.Entities
{
    public class Class : IEntity
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "O campo {0} só pode conter até {1} caracteres.")]
        public string NameClass { get; set; }

        public DateTime BeginSchedule { get; set; }

        public DateTime EndSchedule { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public User User { get; set; }
    }
}
