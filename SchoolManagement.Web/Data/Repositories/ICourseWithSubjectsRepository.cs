using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface ICourseWithSubjectsRepository : IGenericRepository<CourseWithSubjects>
    {
        CourseWithSubjects ToAddCourseWithSubjects(int id, AddSubjectsViewModel model);

        IQueryable<CourseWithSubjects> GetCourseWithSubjects(int idCourse);
    }
}
