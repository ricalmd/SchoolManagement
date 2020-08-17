using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Helpers
{
    public class CourseAndSubjectsHelper : ICourseAndSubjectsHelper
    {
        public CourseSubjectViewModel ToCourseSubjectViewModel(
            Course course,
            IQueryable<Subject> list)
        {
            return new CourseSubjectViewModel
            {
                Id = course.Id,
                Name = course.Name,
                QNQ = course.QNQ,
                QEQ = course.QEQ,
                Profile = course.Profile,
                Reference = course.Reference,
                Subjects = list
            };
        }
    }
}
