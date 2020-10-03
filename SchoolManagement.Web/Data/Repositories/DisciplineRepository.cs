using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Data.Repositories
{
    public class DisciplineRepository : GenericRepository<Discipline>, IDisciplineRepository
    {
        private readonly DataContext _context;

        public DisciplineRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public List<Discipline> GetAllDisciplines()
        {
            return _context.Disciplines.Select(d => d).ToList();
        }

        public List<Discipline> GetDisciplines(int id)
        {
            return _context.CourseWithDisciplines
                .Where(cd => cd.Course.Id.Equals(id))
                .Join(_context.Courses, cd => cd.Course.Id, c => c.Id,
                (cd, c) => new { cwd = cd, courses = c })
                .Join(_context.Disciplines, cd => cd.cwd.DisciplineId, d => d.Id,
                (cd, d) => new { cwd = cd, list = d }).AsNoTracking()
                .Select(x => x.list).AsNoTracking().ToList();
        }

        public List<Discipline> GetDisciplinesFromClass(int id)
        {
            return _context.Classes
                .Where(cl => cl.Id.Equals(id))
                .Join(_context.Courses, cl => cl.CourseId, c => c.Id,
                (cl, c) => new { classes = cl, courses = c })
                .Join(_context.CourseWithDisciplines, c => c.courses.Id, cd => cd.Course.Id,
                (cd, c) => new { cwd = cd, courses = c })
                .Join(_context.Disciplines, cd => cd.courses.DisciplineId, d => d.Id,
                (d, cd) => new { disciplines = d, cwd = cd })
                .Select(x => x.cwd).AsNoTracking().ToList();
        }

        public List<ClassWithDisciplinesViewModel> GetDisciplinesFromTeacher(string id, int disciplineId)
        {
            return (from c in _context.Classes
                    join co in _context.Courses on c.CourseId equals co.Id
                    join cd in _context.CourseWithDisciplines on co.Id equals cd.Course.Id
                    join d in _context.Disciplines on cd.DisciplineId equals d.Id
                    join t in _context.Teachers on d.Id equals t.DisciplineId
                    join u in _context.Users on t.User.Id equals u.Id
                    where u.Id == id && c.Id == disciplineId
                    select new ClassWithDisciplinesViewModel 
                    {
                        Id = d.Id,
                        Code = d.Code,
                        Content = d.Content,
                        Credit = d.Credit,
                        Name = d.Name,
                        Objectiv = d.Objectiv,
                        User = u,
                        Workload = d.Workload,
                        ClassId = c.Id
                    }).AsNoTracking().ToList();
        }
    }
}
