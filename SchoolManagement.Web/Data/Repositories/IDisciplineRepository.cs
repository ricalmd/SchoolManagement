using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface IDisciplineRepository : IGenericRepository<Discipline>
    {
        IQueryable GetAllWithUsers();

        List<Discipline> GetDisciplines(IQueryable<Course> courses, IQueryable<CourseWithDiscipline> cwd, IQueryable<Discipline> list, int id);
    }
}
