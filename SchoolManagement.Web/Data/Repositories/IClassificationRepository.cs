using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface IClassificationRepository : IGenericRepository<Classification>
    {
        List<Classification> GetClassification(
            IQueryable<Student> students,
            IQueryable<CourseWithDiscipline> cwd,
            IQueryable<Discipline> disciplines,
            IQueryable<User> users,
            IQueryable<Class> classes,
            IQueryable<Course> courses,
            IQueryable<Classification> classifications,
            string id);
    }
}
