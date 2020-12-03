using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public class ClassRepository : GenericRepository<Class>, IClassRepository
    {
        private readonly DataContext _context;

        public ClassRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public List<Class> GetClassesFromCourse(int idCourse)
        {
            return _context.Classes.Include(c => c.Course).Where(c => c.CourseId.Equals(idCourse)).ToList();
        }

        public List<Class> GetClassesFromTeacher(string id)
        {
            return (from c in _context.Classes
                    join co in _context.Courses on c.CourseId equals co.Id
                    join cd in _context.CourseWithDisciplines on co.Id equals cd.Course.Id
                    join d in _context.Disciplines on cd.DisciplineId equals d.Id
                    join t in _context.Teachers on d.Id equals t.DisciplineId
                    join u in _context.Users on t.User.Id equals u.Id
                    where u.Id == id
                    select c).Distinct().ToList();
        }

        public List<Class> GetClassesFromUser(string id)
        {
            return _context.Users
                .Where(u => u.Id.Equals(id))
                .Join(_context.Students, u => u.Id, s => s.UserId,
                (s, u) => new { students = s, users = u })
                .Join(_context.Classes, s => s.users.ClassId, c => c.Id,
                (c, s) => new { classes = c, students = s })
                .Select(x => x.students).Include(c => c.Course).ToList();
        }

        public IEnumerable<SelectListItem> GetComboCourses()
        {
            var list = _context.Courses.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Selecionar curso)",
                Value = "0"
            });

            return list;
        }
    }
}
