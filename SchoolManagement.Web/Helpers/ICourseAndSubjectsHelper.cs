using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Helpers
{
    public interface ICourseAndSubjectsHelper
    {
        CourseSubjectViewModel ToCourseSubjectViewModel(
            Course course,
            IQueryable<Subject> subjects);
    }
}
