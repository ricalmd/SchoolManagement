using System.Linq;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface ICourseWithSubjectsRepository : IGenericRepository<CourseWithSubjects>
    {
        IQueryable GetAllWithCoursesNSubjects();
    }
}
