using System.Linq;
using System.Threading.Tasks;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Data.Repositories
{
    public class CourseWithDisciplineRepository : GenericRepository<CourseWithDiscipline>, ICourseWithDisciplineRepository
    {
        private readonly DataContext _context;

        public CourseWithDisciplineRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> DeleteCwdAsync(CourseWithDiscipline courseWithDiscipline)
        {
            if (courseWithDiscipline == null)
            {
                return 0;
            }

            _context.CourseWithDisciplines.Remove(courseWithDiscipline);
            await _context.SaveChangesAsync();
            return courseWithDiscipline.CourseId;
        }

        public IQueryable<CourseWithDiscipline> GetByCourseId(int id)
        {
            return _context.CourseWithDisciplines.Where(c => c.CourseId == id);
        }

        public IQueryable<CourseWithDiscipline> GetCwdAsync(int courseId, int disciplineId)
        {
            return _context.CourseWithDisciplines
                .Where(c => c.DisciplineId.Equals(disciplineId) && c.CourseId.Equals(courseId));
        }

        public CourseWithDiscipline ToAddCourseWithDisciplines(int id, User user, AddDisciplinesViewModel model)
        {
            return new CourseWithDiscipline
            {
                CourseId = id,
                User = user,
                DisciplineId = model.DisciplineId
            };
        }
    }
}
