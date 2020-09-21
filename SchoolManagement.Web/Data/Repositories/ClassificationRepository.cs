using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public class ClassificationRepository : GenericRepository<Classification>, IClassificationRepository
    {
        private readonly DataContext _context;

        public ClassificationRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public List<Classification> GetClassification(
            IQueryable<Student> students, 
            IQueryable<CourseWithDiscipline> cwd, 
            IQueryable<Discipline> disciplines,
            IQueryable<User> users,
            IQueryable<Class> classes,
            IQueryable<Course> courses,
            IQueryable<Classification> classifications,
            string id)
        {
            return users
                .Where(u => u.Id.Equals(id))
                .Join(students, u => u.Id, s => s.UserId,
                (u, s) => new { users = u, students = s })
                .Join(classes, s => s.students.ClassId, c => c.Id,
                (s, c) => new { students = s, classes = c })
                .Join(courses, c => c.classes.CourseId, co => co.Id,
                (c, co) => new { classes = c, courses = co })
                .Join(cwd, co => co.courses.Id, cd => cd.Course.Id,
                (co, cd) => new { courses = co, cwd = cd })
                .Join(disciplines, cd => cd.cwd.DisciplineId, d => d.Id,
                (d, cd) => new { disciplines = d, cwd = cd })
                .Join(classifications, d => d.disciplines.cwd.Discipline.Id, cl => cl.Discipline.Id,
                (d, cl) => new { disciplines = d, cl = classifications })
                .Join(classifications, s => s.disciplines.disciplines.courses.classes.students.students.Id, cl => cl.Student.Id,
                (s, cl) => new { students = s, classifications = cl })
                .Select(x => x.classifications).ToList();
        }
    }
}
