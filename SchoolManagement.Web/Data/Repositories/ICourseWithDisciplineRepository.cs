using System.Linq;
using System.Threading.Tasks;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface ICourseWithDisciplineRepository : IGenericRepository<CourseWithDiscipline>
    {
        CourseWithDiscipline ToAddCourseWithDisciplines(int id, AddDisciplinesViewModel model);

        IQueryable<CourseWithDiscipline> GetByCourseId(int id);

        Task<int> DeleteCwdAsync(CourseWithDiscipline courseWithDiscipline);

        IQueryable<CourseWithDiscipline> GetCwdAsync(int id, CourseAndDisciplinesViewModel courseAndDisciplinesViewModel);
    }
}
