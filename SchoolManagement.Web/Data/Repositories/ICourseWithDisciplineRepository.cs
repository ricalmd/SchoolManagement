using System.Linq;
using System.Threading.Tasks;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface ICourseWithDisciplineRepository : IGenericRepository<CourseWithDiscipline>
    { 
        Task<int> DeleteCwdAsync(CourseWithDiscipline courseWithDiscipline);

        IQueryable<CourseWithDiscipline> GetByCourseId(int id);

        IQueryable<CourseWithDiscipline> GetCwdAsync(int courseId, int disciplineId);

        CourseWithDiscipline ToAddCourseWithDisciplines(int id, User user, AddDisciplinesViewModel model);
    }
}
