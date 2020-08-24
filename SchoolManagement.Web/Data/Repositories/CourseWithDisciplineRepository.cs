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

        public CourseWithDiscipline ToAddCourseWithDisciplines(int id, AddDisciplinesViewModel model)
        {
            return new CourseWithDiscipline
            {
                CourseId = id,
                DisciplineId = model.DisciplineId
            };
        }

        public IQueryable<CourseWithDiscipline> GetByCourseId(int id)
        {
            return _context.CourseWithDisciplines.Where(c => c.CourseId == id);
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

        public IQueryable<CourseWithDiscipline> GetCwdAsync(int id, CourseAndDisciplinesViewModel courseAndDisciplinesViewModel)
        {
            return _context.CourseWithDisciplines
                .Where(c => c.DisciplineId.Equals(id) && c.CourseId.Equals(courseAndDisciplinesViewModel.Id));
        }
    }
}
