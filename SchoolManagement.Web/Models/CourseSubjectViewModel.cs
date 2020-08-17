using System.Collections.Generic;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Models
{
    public class CourseSubjectViewModel : Course
    {
        public IEnumerable<Subject> Subjects { get; set; }
    }
}
