using System.Linq;
using System.Threading.Tasks;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public class CourseWithDisciplineRepository : GenericRepository<CourseWithDiscipline>, ICourseWithDisciplineRepository
    {
        private readonly DataContext _context;

        public CourseWithDisciplineRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteCwdAsync(CourseWithDiscipline courseWithDiscipline)
        {
            if (courseWithDiscipline != null)
            {
                _context.CourseWithDisciplines.Remove(courseWithDiscipline);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<CourseWithDiscipline> GetByCourseId(int id)
        {
            return _context.CourseWithDisciplines.Where(c => c.Course.Id == id);
        }

        public IQueryable<CourseWithDiscipline> GetCwdAsync(int courseId, int disciplineId)
        {
            return _context.CourseWithDisciplines
                .Where(c => c.Discipline.Id.Equals(disciplineId) && c.Course.Id.Equals(courseId));
        }

        public CourseWithDiscipline ToAddCourseWithDisciplines(Course course, User user, int disciplineId)
        {
            return new CourseWithDiscipline
            {
                Course = course,
                User = user,
                DisciplineId = disciplineId
            };
        }
    }
}
