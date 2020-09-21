using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public class DisciplineRepository : GenericRepository<Discipline>, IDisciplineRepository
    {
        private readonly DataContext _context;

        public DisciplineRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Disciplines.Include(d => d.User);
        }

        public List<Discipline> GetDisciplines(
            IQueryable<Course> courses, IQueryable<CourseWithDiscipline> cwd, IQueryable<Discipline> list, int id)
        {
            return cwd
                .Where(cd => cd.Course.Id.Equals(id))
                .Join(courses, cd => cd.Course.Id, c => c.Id,
                (cd, c) => new { cwd = cd, courses = c })
                .Join(list, cd => cd.cwd.DisciplineId, d => d.Id,
                (cd, d) => new { cwd = cd, list = d }).AsNoTracking()
                .Select(x => x.list).ToList();
        }
    }
}
