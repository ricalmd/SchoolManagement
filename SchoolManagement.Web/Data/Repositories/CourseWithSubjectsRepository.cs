using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public class CourseWithSubjectsRepository : GenericRepository<CourseWithSubjects>, ICourseWithSubjectsRepository
    {
        private readonly DataContext _context;

        public CourseWithSubjectsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithCoursesNSubjects()
        {
            return _context.CoursesWithSubjects.Include(c => c.Course).Include(c => c.Subject);
        }
    }
}
